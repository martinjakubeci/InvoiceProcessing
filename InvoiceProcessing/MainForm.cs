using InvoiceProcessing.Data;
using InvoiceProcessing.OCR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InvoiceProcessing
{
    public partial class MainForm : Form
    {
        private readonly string example;
        private InvoiceProcessor _processor = new InvoiceProcessor();

        private JsonSerializerSettings JsonSettings => new JsonSerializerSettings()
        {
            Formatting = Formatting.Indented
        };

        public MainForm()
        {
            InitializeComponent();

            ConfigurationTextBox.Text = JsonConvert.SerializeObject(
                new Configuration()
                {
                    CompanyInfo = new CompanyInfo()
                    {
                        Id = "666666",
                        IsVatRegistered = true,
                        Name = "My s.r.o.",
                        VatId = "SK6666666"
                    },
                    SupplyType = SupplyType.A
                }, JsonSettings);

            example = JsonConvert.SerializeObject(
                new Invoice()
                {
                    ConstSymbol = "308",
                    Created = DateTime.Today.ToString(),
                    Delivered = DateTime.Today.ToString(),
                    Due = DateTime.Today.AddDays(14).ToString(),
                    SpecSymbol = "123",
                    VarSymbol = "456",
                    TotalAmount = 100m,
                    VatAmount = 20m,
                    Supplier = new Supplier()
                    {
                        SupplierOrigin = SupplierOrigin.EU,
                        CompanyInfo = new CompanyInfo()
                        {
                            Id = "123456",
                            IsVatRegistered = true,
                            Name = "Dodavatel s.r.o.",
                            VatId = "SK123456789"
                        }
                    },
                    InvoiceLines = new[]
                    {
                        new InvoiceLine()
                        {
                            Description = "Mydlo",
                            Price = 4m,
                            Qty = 5,
                            Amount = 20m,
                            VatAmount = 4m,
                            ProductType = ProductType.Material
                        },
                        new InvoiceLine()
                        {
                            Description = "Poradenstvo",
                            Price = 80m,
                            Qty = 1,
                            Amount = 80m,
                            VatAmount = 16m,
                            ProductType = ProductType.Service
                        },
                    },
                }, JsonSettings);
        }

        private void LoadImageButton_Click(object sender, EventArgs e)
        {
            using (var dlg = new OpenFileDialog())
                if (dlg.ShowDialog() == DialogResult.OK)
                    InvoicePictureBox.Image = Image.FromFile(dlg.FileName);
        }

        private async void OcrButton_Click(object sender, EventArgs e)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "");  //TODO: add key

                var bytes = ImageToByteArray(InvoicePictureBox.Image);
                var uri = "https://westeurope.api.cognitive.microsoft.com/vision/v1.0/ocr?language=sk&detectOrientation=1";
                HttpResponseMessage response;

                using (ByteArrayContent content = new ByteArrayContent(bytes))
                {
                    content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                    response = await client.PostAsync(uri, content);
                }

                if (response.IsSuccessStatusCode)
                {
                    OcrTextBox.Text = await response.Content.ReadAsStringAsync();
                }
            }
        }

        private byte[] ImageToByteArray(Image img)
        {
            using (var ms = new MemoryStream())
            {
                img.Save(ms, img.RawFormat);
                return ms.ToArray();
            }
        }

        private void ClassifyButton_Click(object sender, EventArgs e)
        {
            //ProcessedTextBox.Text = example;

            var ocrData = JsonConvert.DeserializeObject<OCRRootObject>(OcrTextBox.Text);
            var invoice = TextProcessor.Process(ocrData, JsonConvert.DeserializeObject<Configuration>(ConfigurationTextBox.Text));

            ProcessedTextBox.Text = JsonConvert.SerializeObject(invoice, JsonSettings);
        }

        private void ProcessButton_Click(object sender, EventArgs e)
        {
            var configuration = JsonConvert.DeserializeObject<Configuration>(ConfigurationTextBox.Text);
            var invoice = JsonConvert.DeserializeObject<Invoice>(ProcessedTextBox.Text);

            var accounting = _processor.Process(invoice, configuration);

            AccountingTextBox.Text = string.Join(Environment.NewLine, accounting.Select(a => a.ToString()));//JsonConvert.SerializeObject(accounting, JsonSettings);
        }
    }
}
