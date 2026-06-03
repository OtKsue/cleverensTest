using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cleverensTest
{
    public class LogLevelMapper : ILogLevelMapper
    {
        public string Map(string level) {
            return level?.ToUpperInvariant() switch
            {
                "INFORMATION" => "INFO",
                "WARNING" => "WARN",
                "ERROR" => "ERROR",
                "DEBUG" => "DEBUG",
                "INFO" => "INFO",
                _ => "UNKNOWN"
            };
        }
    }
}
