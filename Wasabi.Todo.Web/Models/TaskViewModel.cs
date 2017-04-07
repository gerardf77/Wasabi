using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Wasabi.Todo.Web.Models
{
    public class TaskViewModel
    {
        public int TaskId { get; set; }

        [Required]
        [Description("The task message")]
        public string Message { get; set; }

        public bool IsCompleted { get; set; }

        public string UserId { get; set; }

        public LocationViewModel Location { get; set; }
    }
}