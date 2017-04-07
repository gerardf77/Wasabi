using Wasabi.Todo.Dtos;
using Wasabi.Todo.Web.Models;
using AutoMapper;

namespace Wasabi.Todo.Web.Core.Mapping
{
    public class WebProfile : Profile
    {
        public WebProfile()
        {
            CreateMap<LocationViewModel, LocationDto>()
                .ForMember(d => d.TaskId, s => s.MapFrom(x => x.TaskId))
                .ForMember(d => d.Latitude, s => s.MapFrom(x => x.Latitude))
                .ForMember(d => d.Longitude, s => s.MapFrom(x => x.Longitude));

            CreateMap<LocationDto, LocationViewModel>()
                .ForMember(d => d.TaskId, s => s.MapFrom(x => x.TaskId))
                .ForMember(d => d.Latitude, s => s.MapFrom(x => x.Latitude))
                .ForMember(d => d.Longitude, s => s.MapFrom(x => x.Longitude));

            CreateMap<TaskViewModel, TaskDto>()
                .ForMember(d => d.UserId, s => s.MapFrom(x => x.UserId))
                .ForMember(d => d.Location, s => s.MapFrom(x => x.Location))
                .ForMember(d => d.IsCompleted, s => s.MapFrom(x => x.IsCompleted))
                .ForMember(d => d.Message, s => s.MapFrom(x => x.Message))
                .ForMember(d => d.TaskId, s => s.MapFrom(x => x.TaskId));

            CreateMap<TaskDto, TaskViewModel>()
                .ForMember(d => d.UserId, s => s.MapFrom(x => x.UserId))
                .ForMember(d => d.Location, s => s.MapFrom(x => x.Location))
                .ForMember(d => d.IsCompleted, s => s.MapFrom(x => x.IsCompleted))
                .ForMember(d => d.Message, s => s.MapFrom(x => x.Message))
                .ForMember(d => d.TaskId, s => s.MapFrom(x => x.TaskId));
        }
    }
}