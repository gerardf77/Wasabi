using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wasabi.Todo.Dtos
{
    public class TaskDto
    {
        public int TaskId { get; set; }

        public string Message { get; set; }

        public bool IsCompleted { get; set; }

        public string UserId { get; set; }

        public LocationDto Location { get; set; }

        public DateTime DateAdded { get; set; }

        public DateTime? DateModified { get; set; }
    }
}
