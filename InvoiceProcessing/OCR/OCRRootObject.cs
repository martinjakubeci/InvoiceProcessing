using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceProcessing.OCR
{
    public abstract class BaseOject
    {
        public string boundingBox
        {
            get => _boundingBox;
            set
            {
                if(_boundingBox != value)
                {
                    _boundingBox = value;
                    BoxObject = BoundingBox.Parse(_boundingBox);
                }
            }
        }

        private string _boundingBox { get; set; }

        public BoundingBox BoxObject { get; set; }
    }

    public class OCRRootObject
    {
        public string language { get; set; }
        public double textAngle { get; set; }
        public string orientation { get; set; }
        public List<Region> regions { get; set; }
    }

    public class Word : BaseOject
    {
        public string text { get; set; }

        public override string ToString() => text;
    }

    public class Line : BaseOject
    {
        public List<Word> words { get; set; }

        public override string ToString() => string.Join(" ", words);
    }

    public class Region : BaseOject
    {
        public List<Line> lines { get; set; }

        public override string ToString() => string.Join(Environment.NewLine, lines);
    }

    public class BoundingBox
    {
        public int Left { get; set; }
        public int Top { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public int Right => Left + Width;
        public int Bottom => Top + Height;

        public static BoundingBox Parse(string str)
        {
            var fields = str.Split(',');

            return new BoundingBox()
            {
                Left = int.Parse(fields[0]),
                Top = int.Parse(fields[1]),
                Width = int.Parse(fields[2]),
                Height = int.Parse(fields[3]),
            };
        }

        public override string ToString() => $"{Left},{Top},{Width},{Height}";
    }
}
