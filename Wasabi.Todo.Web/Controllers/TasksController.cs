using System;
using System.Web.Http;
using System.Web.Http.Description;
using AutoMapper;
using log4net;
using Microsoft.AspNet.Identity;
using Wasabi.Todo.Dtos;
using Wasabi.Todo.Services;
using Wasabi.Todo.Web.Core.Extensions;
using Wasabi.Todo.Web.Core.Mapping;
using Wasabi.Todo.Web.Models;

namespace Wasabi.Todo.Web.Controllers
{
    [Authorize]
    [RoutePrefix("api/tasks")]
    public class TasksController : ApiController
    {
        private readonly ApplicationUserManager userManager;
        private readonly ITaskService taskService;
        private readonly ILog logger;
        private readonly IMapper mapper;


        public TasksController(ApplicationUserManager userManager, ITaskService taskService, ILog logger, IMapper mapper)
        {
            this.userManager = userManager;
            this.taskService = taskService;
            this.logger = logger;
            this.mapper = mapper;
        }

        /// <summary>
        /// Gets the devices.
        /// </summary>
        /// <returns>IEnumerable&lt;DeviceApiViewModel&gt;.</returns>
        [HttpGet]
        public IHttpActionResult GetTasks()
        {
            var tasks = taskService.GetAll();
            return Ok(tasks);
        }

        [HttpGet]
        public IHttpActionResult GetTaskById(int id)
        {
            var tasks = taskService.GetById(id);

            return Ok(tasks);
        }

        [HttpPost]
        [ResponseType(typeof(TaskViewModel))]
        public IHttpActionResult InsertTask(TaskViewModel task)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            task.UserId = User.Identity.GetUserId();

            try
            {
                taskService.AddOrUpdate(task.ProjectDto(mapper));
                taskService.Save();
            }
            catch (AutoMapperMappingException e)
            {
                Console.WriteLine(e);
                return BadRequest(e.Message);
            }

            return Ok(task);
        }

        [HttpPut]
        [ResponseType(typeof(TaskViewModel))]
        public IHttpActionResult UpdateTask(TaskViewModel task)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                taskService.AddOrUpdate(task.ProjectDto(mapper));
                taskService.Save();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return Ok(task);
        }

        [HttpDelete]
        public IHttpActionResult DeleteTask(TaskViewModel task)
        {
            try
            {
                taskService.Delete(task.ProjectDto(mapper));
                taskService.Save();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return Ok(task);
        }
    }
}