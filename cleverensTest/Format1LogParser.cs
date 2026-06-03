using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace cleverensTest
{
    public class Format1LogParser : ILogParser
    {
        private readonly Regex _regex =
        new(@"^(\d{2}\.\d{2}\.\d{4})\s+(\d{2}:\d{2}:\d{2}\.\d{3})\s+(\w+)\s+(.*)$");

        public ParsedLogEntry Parse(string line)
        {
            var match = _regex.Match(line);
            if (!match.Success) return null;

            return new ParsedLogEntry
            {
                SourceDate = match.Groups[1].Value,
                SourceTime = match.Groups[2].Value,
                SourceLogLevel = match.Groups[3].Value,
                SourceMessage = match.Groups[4].Value,
            };
        }
    }
}
