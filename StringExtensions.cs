using System;
using System.Linq;

namespace DevExGridEnhancer
{
    /// <summary>
    /// Provides string extension methods for case-insensitive operations and sorting validation.
    /// </summary>
    internal static class StringExtensions
    {
        /// <summary>
        /// Determines whether the string contains a specified substring while ignoring case.
        /// </summary>
        /// <param name="str">The string to search within.</param>
        /// <param name="substring">The substring to search for.</param>
        /// <returns>true if the substring is found within the string while ignoring case; otherwise, false.</returns>
        public static bool ContainsIgnoringCase(string str, string substring)
        {
            return str.IndexOf(substring, StringComparison.OrdinalIgnoreCase) >= 0;
        }

        /// <summary>
        /// Determines whether the string contains a specified substring while ignoring case.
        /// </summary>
        /// <param name="str">The string to search within.</param>
        /// <param name="substring">The substring to search for.</param>
        /// <returns>true if the substring is found within the string while ignoring case; otherwise, false.</returns>
        public static bool IndexOfIgnoringCase(this string str, string substring)
        {
            return str.IndexOf(substring, StringComparison.OrdinalIgnoreCase) >= 0;
        }

        /// <summary>
        /// Validates a comma-separated list of sort columns.
        /// </summary>
        /// <param name="sort">The comma-separated list of sort columns to validate.</param>
        /// <returns>true if the sort string is valid; otherwise, false.</returns>
        public static bool ValidateSort(this string sort)
        {
            if (string.IsNullOrWhiteSpace(sort)) return true;

            var sorted = sort.Trim();

            if (sorted.All(char.IsLetterOrDigit))
                return true;

            var sortColumns = sorted.Split(',').Select(w => w.Trim());

            foreach (string column in sortColumns)
            {
                var cleanText = column;
                if (column.StartsWith("[", StringComparison.OrdinalIgnoreCase))
                {
                    // Clean up [ and ]
                    cleanText = cleanText.Replace('[', ' ');
                    cleanText = cleanText.Replace(']', ' ');
                }

                if (!cleanText.All(c => char.IsLetterOrDigit(c) || char.IsWhiteSpace(c)))
                    return false;
            }

            return true;
        }
    }
}
