using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace DevExGridEnhancer
{
    /// <summary>
    /// Extension methods for filtering IQueryable data based on DataGridFilterModel.
    /// </summary>
    internal static class LinqWhereExtensions
    {
        /// <summary>
        /// Filters an IQueryable<TEntity> based on a collection of DataGridFilterModel.
        /// </summary>
        /// <typeparam name="TEntity">The entity type.</typeparam>
        /// <param name="source">The IQueryable source to filter.</param>
        /// <param name="filterModels">A collection of filter models specifying the filtering criteria.</param>
        /// <returns>The filtered IQueryable<TEntity>.</returns>
        public static IQueryable<TEntity> Where<TEntity>(this IQueryable<TEntity> source, IEnumerable<DataGridFilterModel> filterModels) where TEntity : class
        {
            // If no filters are provided, return the unfiltered source.
            if (!filterModels.Any()) return source;

            foreach (var filterModel in filterModels)
            {
                // Get the property access expression for the specified field.
                var propertyAccess = PropertySearchHelper.GetPropertyAccess<TEntity>(filterModel.Field);

                if (propertyAccess == null)
                    continue;

                var type = typeof(TEntity);
                var parameter = Expression.Parameter(type, "p");
                var propertyType = propertyAccess.Type;

                var converter = TypeDescriptor.GetConverter(propertyType);

                if (!converter.CanConvertFrom(typeof(string)))
                    throw new NotSupportedException();

                // Get the methods for string.Contains and our custom case-insensitive string comparison method.
                MethodInfo containsMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) });
                MethodInfo containsIgnoringCaseMethod = typeof(StringExtensions).GetMethod("ContainsIgnoringCase", new Type[] { typeof(string), typeof(string) });

                // Convert the filter value to the property's type.
                var propertyValue = converter.ConvertFromInvariantString(filterModel.Value);
                var constant = Expression.Constant(propertyValue);
                var exprRight = Expression.Convert(constant, propertyType);

                // Create expressions for case-insensitive comparison if the property is a string.
                var left = propertyType == typeof(string) ? Expression.Call(propertyAccess, typeof(string).GetMethod("ToLower", Type.EmptyTypes)) : null;
                var right = propertyType == typeof(string) ? Expression.Call(constant, typeof(string).GetMethod("ToLower", Type.EmptyTypes)) : null;

                Expression equalExpr = null;

                // Determine the appropriate filter operation based on the filter model's comparator.
                switch (filterModel.Comparator)
                {
                    case FilterComparator.Equals:
                        equalExpr = left == null ? Expression.Equal(propertyAccess, exprRight) : Expression.Equal(left, right);
                        break;
                    case FilterComparator.NotEqual:
                        equalExpr = left == null ? Expression.NotEqual(propertyAccess, exprRight) : Expression.NotEqual(left, right);
                        break;
                    case FilterComparator.Contains:
                        equalExpr = left == null ? Expression.Call(propertyAccess, containsMethod, exprRight) : Expression.Call(null, containsIgnoringCaseMethod, left, right);
                        break;
                    case FilterComparator.NotContains:
                        equalExpr = left == null ? Expression.Not(Expression.Call(propertyAccess, containsMethod, exprRight)) : Expression.Not(Expression.Call(null, containsIgnoringCaseMethod, left, right));
                        break;
                    case FilterComparator.GreaterThan:
                        equalExpr = Expression.GreaterThan(left, right);
                        break;
                    case FilterComparator.GreaterThanOrEqual:
                        equalExpr = Expression.GreaterThanOrEqual(left, right);
                        break;
                    case FilterComparator.LessThan:
                        equalExpr = Expression.LessThan(left, right);
                        break;
                    case FilterComparator.LessThanOrEqual:
                        equalExpr = Expression.LessThanOrEqual(left, right);
                        break;
                    default:
                        equalExpr = left == null ? Expression.Equal(propertyAccess, exprRight) : Expression.Equal(left, right);
                        break;
                }

                // Create a lambda expression and apply the filter to the source.
                Expression<Func<TEntity, bool>> lambda = Expression.Lambda<Func<TEntity, bool>>(equalExpr, parameter);
                source = source.Where(lambda);
            }

            return source;
        }
    }
}
