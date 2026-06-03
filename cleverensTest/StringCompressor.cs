using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cleverensTest
{
    internal class StringCompressor
    {
        public static string Compress(string input)
        {
            if (!IsValidInput(input))
            {
                throw new ArgumentException(
                    "Строка должна содержать только строчные буквы латинского алфавита (a-z)");
            }

            StringBuilder result = new StringBuilder();
            int count = 1;

            for (int i = 1; i < input.Length; i++)
            {
                if (input[i] == input[i - 1])
                {
                    count++;
                }
                else
                {
                    result.Append(input[i - 1]);

                    if (count > 1)
                        result.Append(count);

                    count = 1;
                }
            }

            result.Append(input[input.Length - 1]);

            if (count > 1)
                result.Append(count);

            return result.ToString();
        }

        public static string Decompress(string input)
        {
            StringBuilder result = new StringBuilder();

            for (int i = 0; i < input.Length; i++)
            {
                char currentChar = input[i];
                int count = 1;

                StringBuilder number = new StringBuilder();

                while (i + 1 < input.Length && char.IsDigit(input[i + 1]))
                {
                    number.Append(input[++i]);
                }

                if (number.Length > 0)
                    count = int.Parse(number.ToString());

                result.Append(new string(currentChar, count));
            }

            return result.ToString();
        }

        private static bool IsValidInput(string input)
        {
            if (string.IsNullOrEmpty(input))
                return false;

            foreach (char c in input)
            {
                if (c < 'a' || c > 'z')
                    return false;
            }

            return true;
        }
    }
}
