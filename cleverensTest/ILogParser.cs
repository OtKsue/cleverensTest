using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cleverensTest
{
    public interface ILogParser
    {
        ParsedLogEntry Parse(string line);
    }
}
