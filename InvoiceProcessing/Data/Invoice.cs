using System;
using System.Collections.Generic;

namespace InvoiceProcessing.Data
{
    public class Invoice
    {
        public Supplier Supplier { get; set; } = new Supplier();
        public string Created { get; set; }
        public string Delivered { get; set; }
        public string Due { get; set; }
        public string ConstSymbol { get; set; }
        public string VarSymbol { get; set; }
        public string SpecSymbol { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal VatAmount { get; set; }
        public string TotalAmountStr { get; set; }
        public string AccountNo { get; set; }

        public IList<InvoiceLine> InvoiceLines { get; set; } = new List<InvoiceLine>();
    }
}
