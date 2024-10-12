using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceProcessing.Helpers
{
    public static class EnumerableExtensions
    {
        public static T Minimize<T>(this IEnumerable<T> @this, Func<T, int> function, T defaultValue = default)
        {
            return @this.Aggregate(defaultValue, (result, next) =>
            {
                if (function(next) < function(result))
                    return next;
                else
                    return result;
            });
        }
    }
}
