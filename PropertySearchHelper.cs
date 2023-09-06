using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace DevExGridEnhancer
{
    /// <summary>
    /// Helper methods for property searching.
    /// </summary>
    internal static class PropertySearchHelper
    {
        /// <summary>
        /// Searches for a property in a given type by name (case-insensitive).
        /// </summary>
        /// <param name="type">The type to search for the property in.</param>
        /// <param name="propertyName">The name of the property to search for.</param>
        /// <returns>The PropertyInfo of the property if found, otherwise null.</returns>
        public static PropertyInfo SearchProperty(Type type, string propertyName)
        {
            if (type == null || string.IsNullOrWhiteSpace(propertyName))
                return null;

            return type.GetProperties()
                .FirstOrDefault(item => item.Name.Equals(propertyName, StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Searches for a property in the type of a PropertyInfo by name (case-insensitive).
        /// </summary>
        /// <param name="propertyInfo">The PropertyInfo whose type will be searched for the property.</param>
        /// <param name="propertyName">The name of the property to search for.</param>
        /// <returns>The PropertyInfo of the property if found, otherwise null.</returns>
        public static PropertyInfo SearchProperty(PropertyInfo propertyInfo, string propertyName)
        {
            if (propertyInfo == null || string.IsNullOrWhiteSpace(propertyName))
                return null;

            return propertyInfo.PropertyType.GetProperties()
                .FirstOrDefault(item => item.Name.Equals(propertyName, StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Searches for a property in a given type by name (case-insensitive).
        /// </summary>
        /// <typeparam name="TEntity">The entity type.</typeparam>
        /// <param name="propertyName">The name of the property to search for.</param>
        /// <returns>The MemberExpression representing the property if found, otherwise null.</returns>
        public static MemberExpression GetPropertyAccess<TEntity>(string propertyName) where TEntity : class
        {
            if (string.IsNullOrWhiteSpace(propertyName))
                return null;

            var type = typeof(TEntity);
            var parameter = Expression.Parameter(type, "p");
            
            PropertyInfo property;
            MemberExpression propertyAccess;
            
            if (propertyName.Contains('.'))
            {
                var childProperties = propertyName.Split('.');
                property = SearchProperty(type, childProperties[0]);

                if (property == null)
                    return null;

                propertyAccess = Expression.MakeMemberAccess(parameter, property);

                for (int i = 1; i < childProperties.Length; i++)
                {
                    property = SearchProperty(property.PropertyType, childProperties[i]);

                    if (property == null)
                        return null;

                    propertyAccess = Expression.MakeMemberAccess(propertyAccess, property);
                }
            }
            else
            {
                property = SearchProperty(type, propertyName);

                if (property == null)
                    return null;

                propertyAccess = Expression.MakeMemberAccess(parameter, property);
            }

            return propertyAccess;
        }
    }
}