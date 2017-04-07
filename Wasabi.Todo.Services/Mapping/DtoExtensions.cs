using AutoMapper;
using Wasabi.Todo.Dtos;
using Wasabi.Todo.Models;

namespace Wasabi.Todo.Services.Mapping
{
    public static class DtoExtensions
    {
        public static Task ProjectToModel(this TaskDto model, IMapper mapper)
        {
            
            return mapper.Map<Task>(model);
        }
    }
}