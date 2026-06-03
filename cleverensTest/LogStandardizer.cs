using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace cleverensTest
{
    public class LogStandardizer
    {

        private readonly CompositeLogParser _parser;
        private readonly ILogFormatter _formatter;
        private readonly ILogLevelMapper _levelMapper;
        private readonly IDateFormatter _dateFormatter;
        private readonly IFileReader _reader;
        private readonly IFileWriter _writer;

        public LogStandardizer(
        CompositeLogParser parser,
        ILogFormatter formatter,
        ILogLevelMapper levelMapper,
        IDateFormatter dateFormatter,
        IFileReader reader,
        IFileWriter writer)

        {
            _parser = parser;
            _formatter = formatter;
            _levelMapper = levelMapper;
            _dateFormatter = dateFormatter;
            _reader = reader;
            _writer = writer;
        }

        public void Process(string input, string output, string bad)
        {
            var lines = _reader.Read(input);

            var good = new List<string>();
            var badLines = new List<string>();

            foreach (var line in lines)
            {
                var parsed = _parser.Parse(line);
                if (parsed == null)
                {
                    badLines.Add(line);
                    continue;
                }

                if (!_dateFormatter.TryFormat(parsed.SourceDate, out var date))
                {
                    badLines.Add(line);
                    continue;
                }

                parsed.SourceDate = date;
                parsed.SourceLogLevel = _levelMapper.Map(parsed.SourceLogLevel);

                good.Add(_formatter.Format(parsed));
            }

            _writer.Write(output, good);

            if (badLines.Any())
                _writer.Write(bad, badLines);
        }
    }
}
