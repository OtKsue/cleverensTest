using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cleverensTest
{
    public class LogFormatter : ILogFormatter
    {
        public string Format(ParsedLogEntry entry)
        {
            return $"{entry.SourceDate}\t{entry.SourceTime}\t{entry.SourceLogLevel}\t" +
               $"{entry.SourceMethod ?? "DEFAULT"}\t{entry.SourceMessage}";
        }
    }
}
