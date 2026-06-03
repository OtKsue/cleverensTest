using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cleverensTest
{
    public class CompositeLogParser
    {
        private readonly List<ILogParser> _parsers;

        public CompositeLogParser(IEnumerable<ILogParser> parsers)
        {
            _parsers = parsers.ToList();
        }

        public ParsedLogEntry Parse(string line)
        {
            return _parsers.Select(p => p.Parse(line)).FirstOrDefault(r => r != null);
        }
    }
}
