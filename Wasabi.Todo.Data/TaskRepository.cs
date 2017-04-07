using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Wasabi.Todo.Data.Extensions;
using Wasabi.Todo.Models;

namespace Wasabi.Todo.Data
{
    public class TaskRepository : ITaskRepository
    {
        private readonly TodoDbContext context;
        /// <summary>
        /// The dbset
        /// </summary>
        protected readonly IDbSet<Task> dbSet;

        public TaskRepository(DbContext context)
        {
            this.context = context as TodoDbContext;
            dbSet = context.Set<Task>();
        }

        public Task GetById(int id)
        {
            return dbSet.AsNoTracking().Include(x => x.Location).FirstOrDefault(x => x.TaskId == id);
        }


        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns>IEnumerable&lt;Task&gt;.</returns>
        public virtual IEnumerable<Task> GetAll()
        {
            return dbSet.AsNoTracking().Include(x => x.Location).AsEnumerable();
        }

        /// <summary>
        /// Finds the by.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        /// <returns>IEnumerable&lt;Task&gt;.</returns>
        public IEnumerable<Task> FindBy(System.Linq.Expressions.Expression<Func<Task, bool>> predicate)
        {
            var query = dbSet.Where(predicate).AsEnumerable();
            return query;
        }

        public Task AddOrUpdate(Task entity)
        {

            if (entity.TaskId == 0)
            {
                context.Entry(entity).State = System.Data.Entity.EntityState.Added;
            }
            else
            {
                context.Entry(entity).State = System.Data.Entity.EntityState.Modified;
            }

            return entity;
        }

        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>Task.</returns>
        public virtual Task Delete(Task entity)
        {
            return dbSet.Remove(entity);
        }

        /// <summary>
        /// Saves this instance.
        /// </summary>
        public virtual void Save()
        {
            context.SaveChanges();
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            context?.Dispose();
        }
    }
}
