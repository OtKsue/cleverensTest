using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cleverensTest
{
    public class ParsedLogEntry
    {
        public string SourceDate { get; set; }
        public string SourceTime { get; set; }
        public string SourceLogLevel { get; set; }
        public string SourceMessage { get; set; }
        public string SourceMethod { get; set; }

    }
}
