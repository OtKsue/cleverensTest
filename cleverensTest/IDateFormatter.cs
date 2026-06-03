using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cleverensTest
{
    public interface IDateFormatter
    {
        bool TryFormat(string input, out string result);
    }
}
