using AutoMapper;
using Wasabi.Todo.Dtos;
using Wasabi.Todo.Web.Core.Mapping;
using Wasabi.Todo.Web.Models;

namespace Wasabi.Todo.Web.Core.Extensions
{
    public static class ViewModelExtensions
    {
        public static TaskDto ProjectDto(this TaskViewModel model, IMapper mapper)
        {
            
            return mapper.Map<TaskDto>(model);
        }
    }
}