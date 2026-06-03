using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cleverensTest
{
    internal class DateFormatter : IDateFormatter
    {
        public bool TryFormat(string input, out string result)
        {
            result = null;

            if (DateTime.TryParseExact(input, "dd.MM.yyyy",
                    CultureInfo.InvariantCulture, DateTimeStyles.None, out var d) ||
                DateTime.TryParseExact(input, "yyyy-MM-dd",
                    CultureInfo.InvariantCulture, DateTimeStyles.None, out d))
            {
                result = d.ToString("dd-MM-yyyy");
                return true;
            }

            return false;
        }
    }
}
