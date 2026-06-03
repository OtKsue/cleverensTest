using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace cleverensTest
{
    public class Format2LogParser : ILogParser
    {
        private readonly Regex _regex =
        new(@"^(\d{4}-\d{2}-\d{2})\s+(\d{2}:\d{2}:\d{2}\.\d+)\|(\s*\w+\s*)\|(\d+)\|([^|]+)\|(.*)$");

        public ParsedLogEntry Parse(string line)
        {
            var match = _regex.Match(line);
            if (!match.Success) return null;

            return new ParsedLogEntry
            {
                SourceDate = match.Groups[1].Value,
                SourceTime = match.Groups[2].Value,
                SourceLogLevel = match.Groups[3].Value.Trim(),
                SourceMethod = match.Groups[5].Value.Trim(),
                SourceMessage = match.Groups[6].Value.Trim()
            };
        }
    }
}
