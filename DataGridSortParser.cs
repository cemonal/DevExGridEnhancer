using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Web;

namespace DevExGridEnhancer
{
    /// <summary>
    /// Helper class for parsing and processing DataGrid sort parameters.
    /// </summary>
    public static class DataGridSortParser
    {
        /// <summary>
        /// Parses the sorting information from a URL-encoded JSON string.
        /// </summary>
        /// <param name="sort">The URL-encoded JSON string containing sorting information.</param>
        /// <returns>An enumerable of <see cref="DataGridSortModel"/>.</returns>
        public static IEnumerable<DataGridSortModel> ParseSorts(string sort)
        {
            if (string.IsNullOrEmpty(sort))
                return Enumerable.Empty<DataGridSortModel>();

            return JsonSerializer.Deserialize<List<DataGridSortModel>>(HttpUtility.UrlDecode(sort, Encoding.UTF8));
        }
    }
}
