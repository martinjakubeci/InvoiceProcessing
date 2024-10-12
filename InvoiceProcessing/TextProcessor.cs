using InvoiceProcessing.Data;
using InvoiceProcessing.Helpers;
using InvoiceProcessing.OCR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceProcessing
{
    public enum MatchingType
    {
        Contains,
        Prefix,
        LineStartsWith,
        Label
    }

    public enum FieldFormat
    {
        Text,
        Numeric,
        Date,
        Decimal
    }

    public class FieldDefinition
    {
        public string[] Title { get; set; }
        public MatchingType MatchingType { get; set; }
        public Lens<Invoice, string> Lens { get; set; }
        public FieldFormat FieldFormat { get; set; }

        public FieldDefinition(string[] title, MatchingType matchingType, Lens<Invoice, string> lens, FieldFormat fieldFormat = FieldFormat.Text)
        {
            Title = title;
            MatchingType = matchingType;
            Lens = lens;
            FieldFormat = fieldFormat;
        }
    }

    public static class TextProcessor
    {
        

        private static FieldDefinition[] fieldMapping = new []
        {
            new FieldDefinition(new [] { "ICO", "IČO" }, MatchingType.Prefix, Lens<Invoice>.New(i => i.Supplier.CompanyInfo.Id)),
            new FieldDefinition(new [] { "DIC", "DIČ" }, MatchingType.Prefix, Lens<Invoice>.New(i => i.Supplier.CompanyInfo.TaxId)),
            new FieldDefinition(new [] { "ICDPH", "IC DPH", "IČDPH", "ICDPH" }, MatchingType.Prefix, Lens<Invoice>.New(i => i.Supplier.CompanyInfo.VatId)),
            new FieldDefinition(new [] { "s.r.o.", "a.s." }, MatchingType.Contains, Lens<Invoice>.New(i => i.Supplier.CompanyInfo.Name)),
            new FieldDefinition(new [] { "SK" }, MatchingType.LineStartsWith, Lens<Invoice>.New(i => i.AccountNo)),
            new FieldDefinition(new [] { "IBAN" }, MatchingType.Contains, Lens<Invoice>.New(i => i.AccountNo)),
            new FieldDefinition(new [] { "Variabilný symbol" }, MatchingType.Label, Lens<Invoice>.New(i => i.VarSymbol)),
            new FieldDefinition(new [] { "Konštantný symbol" }, MatchingType.Label, Lens<Invoice>.New(i => i.ConstSymbol), FieldFormat.Numeric),
            new FieldDefinition(new [] { "Špecifický symbol" }, MatchingType.Label, Lens<Invoice>.New(i => i.SpecSymbol), FieldFormat.Numeric),
            new FieldDefinition(new [] { "Dátum vyhotovenia" }, MatchingType.Label, Lens<Invoice>.New(i => i.Created), FieldFormat.Date),
            new FieldDefinition(new [] { "Dátum splatnosti" }, MatchingType.Label, Lens<Invoice>.New(i => i.Due), FieldFormat.Date),
            new FieldDefinition(new [] { "Dodan" }, MatchingType.Label, Lens<Invoice>.New(i => i.Delivered), FieldFormat.Date),
            new FieldDefinition(new [] { "k úhrade" }, MatchingType.Label, Lens<Invoice>.New(i => i.TotalAmountStr), FieldFormat.Decimal),

        };

        public static Invoice Process(OCRRootObject ocr, Configuration configuration)
        {
            var lines = ocr.regions
                .SelectMany(r => r.lines)
                .ToList();
            var invoice = new Invoice();

            foreach (var mapping in fieldMapping)
            {
                if(mapping.MatchingType == MatchingType.Label)
                {
                    var headers = lines.Where(line => mapping.Title.Any(str => line.ToString().Contains(str)));

                    foreach(var header in headers)
                    {
                        var val = Labels(header.ToString(), header.BoxObject, mapping.FieldFormat, lines.Where(l => l != header));

                        if (val != null)
                        {
                            mapping.Lens.Set(invoice, val);
                            break;
                        }
                    }
                }
                else
                {
                    foreach (var line in lines)
                    {
                        if (MatchAndSet(line, mapping.Lens, mapping.FieldFormat, invoice, mapping.MatchingType, mapping.Title))
                            break;
                    }
                }
            }

            invoice.Supplier.CompanyInfo.IsVatRegistered = !string.IsNullOrWhiteSpace(invoice.Supplier.CompanyInfo.VatId);

            return invoice;
        }

        private static bool MatchAndSet(Line line, Lens<Invoice, string> lens, FieldFormat format, Invoice data, MatchingType matchingType, params string[] patterns)
        {
            var value = MatchLine(line, matchingType, format, patterns);

            if(value != null)
            {
                if (lens.Get(data) == null)
                    lens.Set(data, value);

                return true;
            }

            return false;
        }

        private static string MatchLine(Line line, MatchingType matchingType, FieldFormat format, params string[] patterns)
        {
            //TODO: check format

            if (matchingType == MatchingType.LineStartsWith)
            {
                foreach (var pattern in patterns)
                {
                    if(line.words[0].text.StartsWith(pattern))
                        return string.Join(" ", line.words);
                }

                return null;
            }

            foreach (var pattern in patterns)
                for (int i = 0; i < line.words.Count; i++)
                {
                    var word = line.words[i];

                    if (matchingType == MatchingType.Prefix)
                    {
                        if (word.text.Contains(pattern))
                            return string.Join(" ", line.words.Skip(i + 1));
                    }
                    else if(matchingType == MatchingType.Contains)
                    {
                        if (word.text.Contains(pattern))
                            return string.Join(" ", line.words);
                    }
                }

            return null;
        }

        private static string ExtractNumberFromLine(Line line)
        {
            return "";
        }

        private static string Labels(string text, BoundingBox box, FieldFormat format, IEnumerable<Line> lines)
        {
            var lineList = lines.Where(line => line.ToString() != text).ToList();

            var verticalMatch = lineList
                .Where(line => line.BoxObject.Left <= box.Right && line.BoxObject.Right >= box.Left)
                .Where(line => line.BoxObject.Top > box.Top)
                .Minimize(line => Math.Abs((line?.BoxObject.Top ?? int.MaxValue) - box.Top));

            if (verticalMatch != null && verticalMatch.BoxObject.Top <= box.Bottom + box.Height * 5 
                && MatchesFormat(verticalMatch.ToString(), format))
                return verticalMatch.ToString();

            var horizontalMatch = lineList
                .Where(line => line.BoxObject.Top <= box.Bottom && line.BoxObject.Bottom >= box.Top)
                .Where(line => line.BoxObject.Left > box.Right)
                .Minimize(line => Math.Abs((line?.BoxObject.Left ?? int.MaxValue) - box.Left));

            if (horizontalMatch != null && MatchesFormat(horizontalMatch.ToString(), format))
                return horizontalMatch.ToString();

            return null;
        }

        private static bool MatchesFormat(string value, FieldFormat format)
        {
            if (format == FieldFormat.Text)
                return true;
            else if(format == FieldFormat.Numeric)
            {
                return value.All(c => c.IsNumber());
            }
            else if(format == FieldFormat.Date)
            {
                return value.Count(c => c.IsNumber()) == 8;
            }
            else if(format == FieldFormat.Decimal)
            {
                var parts = value.Split(',', '.');

                return parts.Length == 2 && int.TryParse(parts[0], out int _) && int.TryParse(parts[1], out int _);
            }

            return false;
        }
    }
}
