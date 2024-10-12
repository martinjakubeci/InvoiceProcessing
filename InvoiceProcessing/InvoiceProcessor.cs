using InvoiceProcessing.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceProcessing
{
    public class InvoiceProcessor
    {
        protected List<Rule> _rules = new List<Rule>();

        public InvoiceProcessor()
        {
            _rules = SetupRules().ToList();
        }

        private IEnumerable<Rule> SetupRules()
        {
            //without vat
            //a1 a5
            yield return new Rule(
                (invoice, configuration) => invoice.VatAmount != 0 && configuration.CompanyInfo.IsVatRegistered && configuration.SupplyType == SupplyType.A,
                invoice => new Accounting(Account.a111, null, CalculateMaterialAmount(invoice)),
                invoice => new Accounting(Account.a343, null, invoice.VatAmount),
                invoice => new Accounting(null, Account.a321, -invoice.TotalAmount),
                invoice => new Accounting(Account.a112, null, CalculateMaterialAmount(invoice)),
                invoice => new Accounting(null, Account.a111, -CalculateMaterialAmount(invoice)));

            //a1 n5
            yield return new Rule(
                (invoice, configuration) => invoice.VatAmount != 0 && configuration.CompanyInfo.IsVatRegistered && configuration.SupplyType == SupplyType.B,
                invoice => new Accounting(Account.a501, null, CalculateMaterialAmount(invoice)),
                invoice => new Accounting(Account.a343, null, invoice.VatAmount),
                invoice => new Accounting(null, Account.a321, -invoice.TotalAmount));

            //n1 a5
            yield return new Rule(
                (invoice, configuration) => invoice.VatAmount != 0 && !configuration.CompanyInfo.IsVatRegistered && configuration.SupplyType == SupplyType.A,
                invoice => new Accounting(Account.a111, null, invoice.TotalAmount),
                invoice => new Accounting(null, Account.a321, -invoice.TotalAmount),
                invoice => new Accounting(Account.a112, null, invoice.TotalAmount),
                invoice => new Accounting(null, Account.a111, -invoice.TotalAmount));

            //n1 n5
            yield return new Rule(
                (invoice, configuration) => invoice.VatAmount != 0 && !configuration.CompanyInfo.IsVatRegistered && configuration.SupplyType == SupplyType.B,
                invoice => new Accounting(Account.a501, null, invoice.TotalAmount),
                invoice => new Accounting(null, Account.a321, -invoice.TotalAmount));

            //with vat
            //a1 a2 a3 a5
            yield return new Rule(
                (invoice, configuration) => invoice.VatAmount == 0 && configuration.CompanyInfo.IsVatRegistered && invoice.Supplier.CompanyInfo.IsVatRegistered && invoice.Supplier.SupplierOrigin == SupplierOrigin.EU && configuration.SupplyType == SupplyType.A,
                invoice => new Accounting(Account.a111, null, CalculateMaterialAmount(invoice)),
                invoice => new Accounting(Account.a343, null, CalculateMaterialVatAmount(invoice)),
                invoice => new Accounting(null, Account.a343, CalculateMaterialVatAmount(invoice)),
                invoice => new Accounting(null, Account.a321, -invoice.TotalAmount),
                invoice => new Accounting(Account.a112, null, CalculateMaterialAmount(invoice)),
                invoice => new Accounting(null, Account.a111, -CalculateMaterialAmount(invoice)));

            //a1 a2 a3 n5
            yield return new Rule(
                (invoice, configuration) => invoice.VatAmount == 0 && configuration.CompanyInfo.IsVatRegistered && invoice.Supplier.CompanyInfo.IsVatRegistered && invoice.Supplier.SupplierOrigin == SupplierOrigin.EU && configuration.SupplyType == SupplyType.B,
                invoice => new Accounting(Account.a501, null, CalculateMaterialAmount(invoice)),
                invoice => new Accounting(Account.a343, null, CalculateMaterialVatAmount(invoice)),
                invoice => new Accounting(null, Account.a343, CalculateMaterialVatAmount(invoice)),
                invoice => new Accounting(null, Account.a321, -invoice.TotalAmount));

            //a1 n2 n3 a5
            yield return new Rule(
                (invoice, configuration) => invoice.VatAmount == 0 && configuration.CompanyInfo.IsVatRegistered && !invoice.Supplier.CompanyInfo.IsVatRegistered && invoice.Supplier.SupplierOrigin == SupplierOrigin.OutsideEU && configuration.SupplyType == SupplyType.A,
                invoice => new Accounting(Account.a111, null, invoice.TotalAmount),
                invoice => new Accounting(null, Account.a321, -invoice.TotalAmount));

            //n1 n2 n3 a5
            yield return new Rule(
                (invoice, configuration) => invoice.VatAmount == 0 && !configuration.CompanyInfo.IsVatRegistered && !invoice.Supplier.CompanyInfo.IsVatRegistered && invoice.Supplier.SupplierOrigin == SupplierOrigin.OutsideEU && configuration.SupplyType == SupplyType.A,
                invoice => new Accounting(Account.a112, null, invoice.TotalAmount),
                invoice => new Accounting(null, Account.a111, -invoice.TotalAmount));

            //a1 n2 n3 n5
            yield return new Rule(
                (invoice, configuration) => invoice.VatAmount == 0 && configuration.CompanyInfo.IsVatRegistered && !invoice.Supplier.CompanyInfo.IsVatRegistered && invoice.Supplier.SupplierOrigin == SupplierOrigin.OutsideEU && configuration.SupplyType == SupplyType.B,
                invoice => new Accounting(Account.a501, null, invoice.TotalAmount),
                invoice => new Accounting(null, Account.a321, -invoice.TotalAmount));

            //n1 a2 a3 n5
            yield return new Rule(
                (invoice, configuration) => invoice.VatAmount == 0 && !configuration.CompanyInfo.IsVatRegistered && invoice.Supplier.CompanyInfo.IsVatRegistered && invoice.Supplier.SupplierOrigin == SupplierOrigin.EU && configuration.SupplyType == SupplyType.B,
                invoice => new Accounting(Account.a501, null, invoice.TotalAmount),
                invoice => new Accounting(null, Account.a321, -invoice.TotalAmount));

            //n1 a2 a3 a5
            yield return new Rule(
                (invoice, configuration) => invoice.VatAmount == 0 && !configuration.CompanyInfo.IsVatRegistered && invoice.Supplier.CompanyInfo.IsVatRegistered && invoice.Supplier.SupplierOrigin == SupplierOrigin.EU && configuration.SupplyType == SupplyType.A, 
                invoice => new Accounting(Account.a111, null, CalculateMaterialAmount(invoice)),
                invoice => new Accounting(null, Account.a321, -invoice.TotalAmount),
                invoice => new Accounting(Account.a112, null, CalculateMaterialAmount(invoice)),
                invoice => new Accounting(null, Account.a111, -CalculateMaterialAmount(invoice)));
            
            //n1 a2 a3 n5
            yield return new Rule(
                (invoice, configuration) => invoice.VatAmount == 0 && !configuration.CompanyInfo.IsVatRegistered && invoice.Supplier.CompanyInfo.IsVatRegistered && invoice.Supplier.SupplierOrigin == SupplierOrigin.EU && configuration.SupplyType == SupplyType.B,
                invoice => new Accounting(Account.a501, null, CalculateMaterialAmount(invoice)),
                invoice => new Accounting(null, Account.a321, -invoice.TotalAmount));

        }

        public IEnumerable<Accounting> Process(Invoice invoice, Configuration configuration)
        {
            foreach(var rule in _rules)
            {
                if (rule.Condition(invoice, configuration))
                {
                    foreach (var effect in rule.Effects)
                        yield return effect(invoice);
                }
            }
        }

        private static decimal CalculateMaterialAmount(Invoice invoice)
        {
            return invoice.InvoiceLines
                .Where(l => l.ProductType == ProductType.Material)
                .Sum(l => l.Amount);
        }

        private static decimal CalculateMaterialVatAmount(Invoice invoice)
        {
            return invoice.InvoiceLines
                .Where(l => l.ProductType == ProductType.Material)
                .Sum(l => l.VatAmount);
        }
    }

    public class Rule
    {
        public Func<Invoice, Configuration, bool> Condition { get; set; }
        public Func<Invoice, Accounting>[] Effects { get; set; }

        public Rule(Func<Invoice, Configuration, bool> condition, params Func<Invoice, Accounting>[] effects)
        {
            Condition = condition;
            Effects = effects;
        }
    }
}
