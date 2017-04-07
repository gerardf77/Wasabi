using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Wasabi.Todo.Models;

namespace Wasabi.Todo.Data
{
    public interface ITaskRepository
    {
        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns>IEnumerable&lt;Task&gt;.</returns>
        Task GetById(int id);
        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns>IEnumerable&lt;Task&gt;.</returns>
        IEnumerable<Task> GetAll();
        /// <summary>
        /// Finds the by.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        /// <returns>IEnumerable&lt;Task&gt;.</returns>
        IEnumerable<Task> FindBy(Expression<Func<Task, bool>> predicate);
        /// <summary>
        /// Adds the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>Task.</returns>
        Task AddOrUpdate(Task entity);
        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>Task.</returns>
        Task Delete(Task entity);
        /// <summary>
        /// Saves this instance.
        /// </summary>
        void Save();
    }
}