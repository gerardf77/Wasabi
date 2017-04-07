using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wasabi.Todo.Models
{
    public class TaskPermission
    {
        [ForeignKey("Task")]
        public int TaskId { get; set; }

        public string UserId { get; set; }

        public Task Task { get; set; }
    }
}
