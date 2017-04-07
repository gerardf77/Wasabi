using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Linq.Expressions;

namespace Wasabi.Todo.Data.Extensions
{
    /// <summary>
    /// Class EntityFrameworkExtensions.
    /// </summary>
    public static class EntityFrameworkExtensions
    {
        /// <summary>
        /// Includes the multiple.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query">The query.</param>
        /// <param name="includes">The includes.</param>
        /// <returns>IQueryable&lt;T&gt;.</returns>
        public static IQueryable<T> IncludeMultiple<T>(this IQueryable<T> query, params Expression<Func<T, object>>[] includes)
            where T : class
        {
            if (includes != null)
            {
                query = includes.Aggregate(query,
                          (current, include) => current.Include(include));
            }

            return query;
        }

        public static object[] KeyValuesFor(this DbContext context, object entity)
        {
            Contract.Requires(context != null);
            Contract.Requires(entity != null);

            var entry = context.Entry(entity);
            return context.KeysFor(entity.GetType())
                .Select(k => entry.Property(k).CurrentValue)
                .ToArray();
        }

        public static IEnumerable<string> KeysFor(this DbContext context, Type entityType)
        {
            Contract.Requires(context != null);
            Contract.Requires(entityType != null);

            entityType = ObjectContext.GetObjectType(entityType);

            var metadataWorkspace =
                ((IObjectContextAdapter)context).ObjectContext.MetadataWorkspace;
            var objectItemCollection =
                (ObjectItemCollection)metadataWorkspace.GetItemCollection(DataSpace.OSpace);

            var ospaceType = metadataWorkspace
                .GetItems<EntityType>(DataSpace.OSpace)
                .SingleOrDefault(t => objectItemCollection.GetClrType(t) == entityType);

            if (ospaceType == null)
            {
                throw new ArgumentException($"The type '{entityType.Name}' is not mapped as an entity type.", nameof(entityType));
            }

            return ospaceType.KeyMembers.Select(k => k.Name);
        }

        public static bool IsDefaultValue(object keyValue)
        {
            return keyValue == null
                   || (keyValue.GetType().IsValueType
                       && Equals(Activator.CreateInstance(keyValue.GetType()), keyValue));
        }
    }
}
