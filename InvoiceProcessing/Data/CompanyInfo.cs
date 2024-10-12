namespace InvoiceProcessing.Data
{
    public class CompanyInfo
    {
        public string Name { get; set; }
        public string Id { get; set; }
        public string TaxId { get; set; }
        public string VatId { get; set; }
        public bool IsVatRegistered { get; set; }
    }
}
