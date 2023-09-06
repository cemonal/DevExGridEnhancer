namespace DevExGridEnhancer
{
    /// <summary>
    /// Specifies the comparison operators used for filtering.
    /// </summary>
    public enum FilterComparator
    {
        /// <summary>
        /// Represents the equality (==) comparison operator.
        /// </summary>
        Equals,

        /// <summary>
        /// Represents the inequality (!=) comparison operator.
        /// </summary>
        NotEqual,

        /// <summary>
        /// Represents the contains comparison operator for string matching.
        /// </summary>
        Contains,

        /// <summary>
        /// Represents the does not contain comparison operator for string matching.
        /// </summary>
        NotContains,

        /// <summary>
        /// Represents the greater than (>) comparison operator.
        /// </summary>
        GreaterThan,

        /// <summary>
        /// Represents the greater than or equal to (>=) comparison operator.
        /// </summary>
        GreaterThanOrEqual,

        /// <summary>
        /// Represents the less than (<) comparison operator.
        /// </summary>
        LessThan,

        /// <summary>
        /// Represents the less than or equal to (<=) comparison operator.
        /// </summary>
        LessThanOrEqual
    }
}
