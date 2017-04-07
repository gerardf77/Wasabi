using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Wasabi.Todo.Data;
using Wasabi.Todo.Dtos;
using Wasabi.Todo.Models;
using Wasabi.Todo.Services.Mapping;

namespace Wasabi.Todo.Services
{
    public class TaskService : ITaskService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ITaskRepository repository;
        private readonly IMapper mapper;

        public TaskService(IUnitOfWork unitOfWork, ITaskRepository repository, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.repository = repository;
            this.mapper = mapper;
        }

        public TaskDto GetById(int id)
        {
            return mapper.Map<TaskDto>(repository.GetById(id));
        }

        /// <summary>
        /// Adds the specified dto.
        /// </summary>
        /// <param name="dto">The dto.</param>
        /// <exception cref="System.ArgumentNullException">dto</exception>
        public virtual void AddOrUpdate(TaskDto dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto));
            }

            if (dto.TaskId == 0)
            {
                dto.DateAdded = DateTime.Now;
            }
            else
            {
                dto.DateModified = DateTime.Now;
            }

            //var newTask = dto.ProjectToModel(mapper);
            //if (dto.TaskId != 0)
            //{
            //    var oldTask = GetById(dto.TaskId);
            //    newTask.DateAdded = oldTask.DateAdded;
            //    newTask.DateModified = DateTime.Now;
            //    newTask.Location.DateAdded = oldTask.DateAdded;
            //    newTask.Location.DateModified = DateTime.Now;
            //}
            //else
            //{
            //    newTask.DateAdded = DateTime.Now;
            //    newTask.Location.DateAdded = DateTime.Now;
            //}
            repository.AddOrUpdate(dto.ProjectToModel(mapper));
        }

        /// <summary>
        /// Deletes the specified dto.
        /// </summary>
        /// <param name="dto">The dto.</param>
        /// <exception cref="System.ArgumentNullException">dto</exception>
        public virtual void Delete(TaskDto dto)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));
            repository.Delete(mapper.Map<Task>(dto));
        }

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns>IEnumerable&lt;T&gt;.</returns>
        public virtual IEnumerable<TaskDto> GetAll()
        {
            return mapper.Map<List<TaskDto>>(repository.GetAll().ToList());
        }

        /// <summary>
        /// Saves this instance.
        /// </summary>
        public void Save()
        {
            unitOfWork.Commit();
        }
    }
}
