using System;
using System.ComponentModel.DataAnnotations;

namespace Wasabi.Todo.Models
{
    public abstract class BaseEntity
    {
        [DataType(DataType.DateTime)]
        public DateTime DateAdded { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? DateModified { get; set; }
    }
}
