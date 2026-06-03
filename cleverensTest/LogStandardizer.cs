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
        public class ParsedLogEntry
        {
            public string SourceDate { get; set; }
            public string SourceTime { get; set; }
            public string SourceLogLevel { get; set; }
            public string SourceMessage { get; set; }
            public string SourceMethod { get; set; } 
        }

        
        private static readonly Regex _format1Regex = new Regex(
            @"^(\d{2}\.\d{2}\.\d{4})\s+(\d{2}:\d{2}:\d{2}\.\d{3})\s+(\w+)\s+(.*)$");

        private static readonly Regex _format2Regex = new Regex(
            @"^(\d{4}-\d{2}-\d{2})\s+(\d{2}:\d{2}:\d{2}\.\d+)\|(\s*\w+\s*)\|(\d+)\|([^|]+)\|(.*)$");

        // Метод для парсинга одной строки лога
        private static ParsedLogEntry ParseLogLine(string line)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                return null;
            }

            var match1 = _format1Regex.Match(line);
            if (match1.Success)
            {
                return new ParsedLogEntry
                {
                    SourceDate = match1.Groups[1].Value,
                    SourceTime = match1.Groups[2].Value,
                    SourceLogLevel = match1.Groups[3].Value,
                    SourceMessage = match1.Groups[4].Value.Trim(),
                    SourceMethod = null 
                };
            }

            var match2 = _format2Regex.Match(line);
            if (match2.Success)
            {
                return new ParsedLogEntry
                {
                    SourceDate = match2.Groups[1].Value,
                    SourceTime = match2.Groups[2].Value,
                    SourceLogLevel = match2.Groups[3].Value.Trim(),
                    SourceMethod = match2.Groups[5].Value.Trim(),
                    SourceMessage = match2.Groups[6].Value.Trim()
                };
            }

            // Если ни один формат не подошел
            Console.WriteLine($"Предупреждение: Не удалось распознать строку: {line}");
            return null;
        }

       
        private static string MapLogLevel(string level)
        {
            level = level?.ToUpperInvariant();
            switch (level)
            {
                case "INFORMATION":
                    return "INFO";
                case "WARNING":
                    return "WARN";
                case "ERROR":
                    return "ERROR";
                case "DEBUG":
                    return "DEBUG";
                case "INFO": 
                    return "INFO";
                default:
                    return "UNKNOWN";
            }
        }

        private static string FormatDate(string sourceDate, out bool isValid)
        {
            isValid = false;
            if (string.IsNullOrWhiteSpace(sourceDate)) return null;

            DateTime dateObj;
            if (DateTime.TryParseExact(sourceDate, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateObj) ||
                DateTime.TryParseExact(sourceDate, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateObj))
            {
                isValid = true;
                return dateObj.ToString("dd-MM-yyyy", CultureInfo.InvariantCulture);
            }

            Console.WriteLine($"Ошибка форматирования даты '{sourceDate}'. Запись будет считаться невалидной.");
            return null;
        }

        private static string StandardizeEntry(ParsedLogEntry entry, string problemsFilePath)
        {
            if (entry == null)
            {
                return null;
            }

            bool isDateValid = false;
            string formattedDate = FormatDate(entry.SourceDate, out isDateValid);

            if (!isDateValid || formattedDate == null)
            {
                string originalProblemLine = $"{entry.SourceDate} {entry.SourceTime} {entry.SourceLogLevel} {(entry.SourceMethod ?? "")} {entry.SourceMessage}".Trim();
                return null;
            }

            string standardizedLevel = MapLogLevel(entry.SourceLogLevel);
            if (standardizedLevel == "UNKNOWN")
            {
                Console.WriteLine($"Предупреждение: Неизвестный уровень логирования '{entry.SourceLogLevel}'. Использование 'UNKNOWN'.");
            }

            string methodName = string.IsNullOrEmpty(entry.SourceMethod) ? "DEFAULT" : entry.SourceMethod;

            string message = entry.SourceMessage;

            return $"{formattedDate}\t{entry.SourceTime}\t{standardizedLevel}\t{methodName}\t{message}";
        }

        public static void ProcessLogFile(string inputFilePath, string outputFilePath, string problemsFilePath)
        {
            try
            {
                string[] lines = File.ReadAllLines(inputFilePath, Encoding.UTF8);
                var standardizedEntries = new List<string>();
                var problematicEntries = new List<string>();

                foreach (string line in lines)
                {
                    string originalLine = line.Trim(); 
                    if (string.IsNullOrWhiteSpace(originalLine))
                    {
                        continue;
                    }

                    var parsedEntry = ParseLogLine(originalLine);

                    if (parsedEntry != null)
                    {
                        string standardizedLine = StandardizeEntry(parsedEntry, originalLine); 
                        if (standardizedLine != null)
                        {
                            standardizedEntries.Add(standardizedLine);
                        }
                        else
                        {
                            problematicEntries.Add(originalLine);
                        }
                    }
                    else
                    {
                        problematicEntries.Add(originalLine);
                    }
                }

                File.WriteAllLines(outputFilePath, standardizedEntries, Encoding.UTF8);

                if (problematicEntries.Count > 0)
                {
                    File.WriteAllLines(problemsFilePath, problematicEntries, Encoding.UTF8);
                    Console.WriteLine($"Найдены невалидные или нераспознанные записи. Они сохранены в: {problemsFilePath}");
                }
                else
                {
                    Console.WriteLine("Невалидных или нераспознанных записей не найдено.");
                }

                Console.WriteLine($"Лог-файл успешно обработан. Стандартизированные записи сохранены в: {outputFilePath}");
            }
            catch (FileNotFoundException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Ошибка: Входной файл '{inputFilePath}' не найден.");
                Console.ResetColor();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Произошла ошибка при обработке файла: {ex.Message}");
                Console.ResetColor();
            }
        }
    }
}
