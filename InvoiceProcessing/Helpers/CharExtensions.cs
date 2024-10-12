namespace InvoiceProcessing.Helpers
{
    public static class CharExtensions
    {
        public static bool IsNumber(this char @this) => @this >= '0' && @this <= '9';
    }
}
