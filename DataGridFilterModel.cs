namespace DevExGridEnhancer
{
    /// <summary>
    /// Represents a filter model used for filtering data in a data grid.
    /// </summary>
    public class DataGridFilterModel
    {
        /// <summary>
        /// Gets or sets the comparator used for filtering.
        /// </summary>
        public FilterComparator Comparator { get; set; }

        /// <summary>
        /// Gets or sets the field or property name to be filtered.
        /// </summary>
        public string Field { get; set; }

        /// <summary>
        /// Gets or sets the value to filter by.
        /// </summary>
        public string Value { get; set; }
    }
}
