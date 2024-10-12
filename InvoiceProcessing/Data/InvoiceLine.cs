namespace InvoiceProcessing.Data
{
    public class InvoiceLine
    {
        public string Description { get; set; }
        public int Qty { get; set; }
        public decimal Price { get; set; }
        public decimal VatAmount { get; set; }
        public decimal Amount { get; set; }
        public ProductType ProductType { get; set; }
    }
}
