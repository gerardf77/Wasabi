using System;
using System.Collections.Generic;
using Wasabi.Todo.Dtos;
using Wasabi.Todo.Models;

namespace Wasabi.Todo.Services
{
    public interface ITaskService : IEntityService
    {
        /// <summary>
        /// Gets by id.
        /// </summary>
        /// <returns>IEnumerable&lt;T&gt;.</returns>
        TaskDto GetById(int id);

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns>IEnumerable&lt;T&gt;.</returns>
        IEnumerable<TaskDto> GetAll();

        /// <summary>
        /// Adds the or update.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void AddOrUpdate(TaskDto entity);

        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void Delete(TaskDto entity);
    }
}
