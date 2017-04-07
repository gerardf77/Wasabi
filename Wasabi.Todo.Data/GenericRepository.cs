using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Wasabi.Todo.Data.Extensions;
using Wasabi.Todo.Models;

namespace Wasabi.Todo.Data
{
    public abstract class GenericRepository<T> : IGenericRepository<T>, IDisposable
           where T : BaseEntity
    {
        /// <summary>
        /// The database context
        /// </summary>
        protected DbContext DbContext;
        /// <summary>
        /// The dbset
        /// </summary>
        protected readonly IDbSet<T> DbSet;

        /// <summary>
        /// Initializes a new instance of the <see cref="GenericRepository{T}"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        protected GenericRepository(DbContext context)
        {
            DbContext = context;
            DbSet = context.Set<T>();
        }

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns>IEnumerable&lt;T&gt;.</returns>
        public virtual IEnumerable<T> GetAll()
        {
            return DbSet.AsEnumerable();
        }

        /// <summary>
        /// Finds the by.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        /// <returns>IEnumerable&lt;T&gt;.</returns>
        public IEnumerable<T> FindBy(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            var query = DbSet.Where(predicate).AsEnumerable();
            return query;
        }

        public T AddOrUpdate(T entity)
        {
            DbContext.Entry(entity).State =
                    DbContext.KeyValuesFor(entity).All(EntityFrameworkExtensions.IsDefaultValue)
                        ? EntityState.Added
                        : EntityState.Modified;

            return entity;
        }

        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>T.</returns>
        public virtual T Delete(T entity)
        {
            return DbSet.Remove(entity);
        }

        /// <summary>
        /// Saves this instance.
        /// </summary>
        public virtual void Save()
        {
            DbContext.SaveChanges();
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            DbContext?.Dispose();
        }
    }
}
