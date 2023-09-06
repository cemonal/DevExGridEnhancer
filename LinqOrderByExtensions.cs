using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DevExGridEnhancer
{
    /// <summary>
    /// Provides extensions for ordering <see cref="IQueryable{TEntity}"/> data sources based on sorting models.
    /// </summary>
    public static class LinqOrderByExtensions
    {
        /// <summary>
        /// Orders an <see cref="IQueryable{TEntity}"/> data source based on the provided sorting models.
        /// </summary>
        /// <typeparam name="TEntity">The entity type.</typeparam>
        /// <param name="source">The source queryable.</param>
        /// <param name="sortModels">The sorting models to apply.</param>
        /// <returns>An ordered queryable.</returns>
        /// <remarks>
        /// This extension method allows you to order an IQueryable data source based on the provided sorting models.
        /// It takes a sequence of sorting models and applies the specified sorting orders to the IQueryable data source.
        /// </remarks>
        /// <example>
        /// <code>
        /// // Define a list of sorting models
        /// var sortModels = new List<DataGridSortModel>
        /// {
        ///     new DataGridSortModel { PropertySelector = "PropertyName", IsDescending = false },
        ///     new DataGridSortModel { PropertySelector = "AnotherProperty", IsDescending = true }
        /// };
        /// 
        /// // Apply sorting to an IQueryable data source
        /// var orderedQuery = queryableSource.OrderBy(sortModels);
        /// </code>
        /// </example>
        public static IQueryable<TEntity> OrderBy<TEntity>(this IQueryable<TEntity> source, IEnumerable<DataGridSortModel> sortModels) where TEntity : class
        {
            if (sortModels == null || !sortModels.Any()) return source;

            var queryExpr = source.Expression;
            var type = typeof(TEntity);
            var parameter = Expression.Parameter(type, "p");

            foreach (var sortModel in sortModels)
            {
                var command = sortModel.IsDescending ? "OrderByDescending" : "OrderBy";

                var propertyName = sortModel.PropertySelector.Trim();
                var propertyAccess = PropertySearchHelper.GetPropertyAccess<TEntity>(propertyName);

                if (propertyAccess != null)
                {
                    var orderByExpression = Expression.Lambda(propertyAccess, parameter);
                    queryExpr = Expression.Call(typeof(Queryable), command, new[] { type, propertyAccess.Type }, queryExpr, Expression.Quote(orderByExpression));
                }
            }

            return source.Provider.CreateQuery<TEntity>(queryExpr);
        }
    }
}
