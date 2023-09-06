using System.Collections.Generic;
using System.Linq;

namespace DevExGridEnhancer
{
    /// <summary>
    /// Extensions for working with DataGrid sorting and filtering.
    /// </summary>
    internal static class DataGridExtensions
    {
        /// <summary>
        /// Convert a list of <see cref="DataGridSortModel"/> objects to a sorting string.
        /// </summary>
        /// <param name="sorts">List of <see cref="DataGridSortModel"/> objects.</param>
        /// <returns>A string representing sorting criteria.</returns>
        public static string ConvertToSortString(IEnumerable<DataGridSortModel> sorts)
        {
            if (sorts == null || !sorts.Any()) return string.Empty;

            var sortedProperties = sorts.Select(sort => $"{sort.PropertySelector} {(sort.IsDescending ? "DESC" : "ASC")}").ToList();

            return string.Join(", ", sortedProperties);
        }

        /// <summary>
        /// Converts a nested property expression to a nullable nested expression.
        /// </summary>
        /// <param name="expression">The nested property expression.</param>
        /// <returns>The nullable nested expression.</returns>
        public static string ConvertToNullableNested(string expression)
        {
            if (string.IsNullOrEmpty(expression)) return string.Empty;

            var properties = expression.Split('.');
            var result = expression;

            for (int index = 0; index < properties.Length - 1; index++)
            {
                var property = string.Join(".", properties.Take(index + 1));
                result = result.Replace(expression, $"({property} != null ? {expression} : null)");
            }

            return result;
        }
    }
}
