using System;
using System.Web.Http;
using System.Web.Http.Description;
using log4net;
using Microsoft.AspNet.Identity;
using Wasabi.Todo.Services;

namespace Wasabi.Todo.WebApi.Controllers
{
    [RoutePrefix("api/tasks")]
    public class TasksController : ApiController
    {
        private readonly ApplicationUserManager userManager;
        private readonly ITaskService taskService;
        private readonly ILog logger;

        public TasksController(ApplicationUserManager userManager, ITaskService taskService, ILog logger)
        {
            this.userManager = userManager;
            this.taskService = taskService;
            this.logger = logger;
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
        [ResponseType(typeof(Todo.Models.Task))]
        public IHttpActionResult InsertTask(Todo.Models.Task task)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            task.UserId = User.Identity.GetUserId();
            task.DateAdded = DateTime.Now;

            try
            {
                taskService.AddOrUpdate(task);
                taskService.Save();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }


            return Ok(task);
        }

        [HttpPut]
        [ResponseType(typeof(Todo.Models.Task))]
        public IHttpActionResult UpdateTask(Todo.Models.Task task)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                taskService.AddOrUpdate(task);
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
        public IHttpActionResult DeleteTask(Wasabi.Todo.Models.Task task)
        {
            try
            {
                taskService.Delete(task);
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