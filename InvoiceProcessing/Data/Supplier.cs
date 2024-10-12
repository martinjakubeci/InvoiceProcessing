namespace InvoiceProcessing.Data
{
    public class Supplier
    {
        public SupplierOrigin SupplierOrigin { get; set; }
        public CompanyInfo CompanyInfo { get; set; } = new CompanyInfo();

    }
}
