using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Text.Json;
using System.Web;

namespace DevExGridEnhancer
{
    /// <summary>
    /// Helper class for parsing and processing DataGrid filter parameters.
    /// </summary>
    public static class DataGridFilterParser
    {
        /// <summary>
        /// Parses the filtering information from a URL-encoded JSON string.
        /// </summary>
        /// <param name="filter">The URL-encoded JSON string containing filtering information.</param>
        /// <returns>An enumerable of <see cref="DataGridFilterModel"/>.</returns>
        public static IEnumerable<DataGridFilterModel> ParseFilters(string filter)
        {
            if (string.IsNullOrEmpty(filter))
                return Enumerable.Empty<DataGridFilterModel>();

            var filterArray = JsonSerializer.Deserialize<JsonArray>(HttpUtility.UrlDecode(filter, Encoding.UTF8));
            return ParseFilters(filterArray);
        }

        /// <summary>
        /// Parses the filtering information from a JSON array.
        /// </summary>
        /// <param name="filterArray">The JSON array containing filtering information.</param>
        /// <returns>An enumerable of <see cref="DataGridFilterModel"/>.</returns>
        public static IEnumerable<DataGridFilterModel> ParseFilters(JsonArray filterArray)
        {
            if (filterArray == null)
                return Enumerable.Empty<DataGridFilterModel>();

            return FlattenArray(filterArray).Select(f => new DataGridFilterModel { Field = f[0].Trim('"'), Comparator = ParseComparator(f[1].Trim('"')), Value = f[2].Trim('"') });
        }

        private static FilterComparator ParseComparator(string pComparator)
        {
            if (pComparator == "=")
                return FilterComparator.Equals;
            else if (pComparator == "contains")
                return FilterComparator.Contains;
            else if (pComparator == ">")
                return FilterComparator.GreaterThan;
            else if (pComparator == ">=")
                return FilterComparator.GreaterThanOrEqual;
            else if (pComparator == "<")
                return FilterComparator.LessThan;
            else if (pComparator == "<=")
                return FilterComparator.LessThanOrEqual;
            else if (pComparator == "notcontains")
                return FilterComparator.NotContains;
            else if (pComparator == "!=" || pComparator == "<>")
                return FilterComparator.NotEqual;
            else
                throw new NotSupportedException("Unsupported filter comparator: " + pComparator);
        }

        private static IEnumerable<string[]> FlattenArray(JsonArray filters)
        {
            if (filters.Any(f => f is JsonArray))
            {
                foreach (var filter in filters.Where(f => f is JsonArray).Cast<JsonArray>()) // we drop all "and"s
                {
                    if (filter.All(f => !(f is JsonArray)))
                        yield return filter.Select(o => DateTime.TryParse(o.ToString(), out DateTime dateTime) ? dateTime.ToString("yyyy-MM-ddTHH:mm:ss.fffZ") : o.ToString()).ToArray();
                    else
                        foreach (var subFilter in FlattenArray(filter))
                            yield return subFilter;
                }
            }
            else
            {
                yield return filters.Select(o => DateTime.TryParse(o.ToString(), out DateTime dateTime) ? dateTime.ToString("yyyy-MM-ddTHH:mm:ss.fffZ") : o.ToString()).ToArray();
            }
        }
    }
}
