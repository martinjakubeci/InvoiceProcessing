using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceProcessing.Data
{
    public class Accounting
    {
        public Account? DebitAccount { get; set; }
        public Account? CreditAccount { get; set; }
        public decimal Amount { get; set; }

        public Accounting()
        {
        }

        public Accounting(Account? debit, Account? credit, decimal amount)
        {
            DebitAccount = debit;
            CreditAccount = credit;
            Amount = amount;
        }

        public override string ToString()
        {
            return $"{FormatAccount(DebitAccount)}\t{FormatAccount(CreditAccount)}\t{Amount.ToString("F")}";
        }

        private static string FormatAccount(Account? account)
        {
            return account?.ToString().Replace("a", string.Empty) ?? "X";
        }
    }

    public enum Account
    {
        a111,
        a112,
        a321,
        a343,
        a501
    }
}
