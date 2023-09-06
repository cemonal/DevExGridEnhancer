using System.Text.Json.Serialization;

namespace DevExGridEnhancer
{
    /// <summary>
    /// Represents a sorting model for the DataGrid.
    /// </summary>
    public class DataGridSortModel
    {
        /// <summary>
        /// Gets or sets a value indicating whether sorting is in descending order.
        /// </summary>
        [JsonPropertyName("desc")]
        public bool IsDescending { get; set; }

        /// <summary>
        /// Gets or sets the property selector for sorting.
        /// </summary>
        [JsonPropertyName("selector")]
        public string PropertySelector { get; set; }
    }
}
