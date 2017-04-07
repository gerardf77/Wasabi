using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Wasabi.Todo.Models
{
    public class Task : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TaskId { get; set; }

        public string UserId { get; set; }

        [Required]
        public string Message { get; set; }

        public bool IsCompleted { get; set; }

        public virtual Location Location { get; set; }
    }
}
