using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wasabi.Todo.Models
{
    public class Location : BaseEntity
    {
        [Key]
        [ForeignKey("Task")]
        public int TaskId { get; set; }

        
        public string Latitude { get; set; }

        public string Longitude { get; set; }

        public virtual Task Task { get; set; }
    }
}
