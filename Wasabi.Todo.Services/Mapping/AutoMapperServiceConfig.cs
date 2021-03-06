﻿using AutoMapper;
using Wasabi.Todo.Core.Extensions;
using Wasabi.Todo.Dtos;
using Wasabi.Todo.Models;

namespace Wasabi.Todo.Services.Mapping
{
    public class AutoMapperServicesConfig
    {
        public static IMapper Mapper;
        public static void ConfigureAutoMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });

            Mapper = config.CreateMapper();
            config.AssertConfigurationIsValid();
        }
    }

    public class MappingProfile : Profile
    {
        protected override void Configure()
        {

            CreateMap<Location, LocationDto>()
                .IgnoreAllUnmapped()
                .ForMember(d => d.TaskId, s => s.MapFrom(x => x.TaskId))
                .ForMember(d => d.Latitude, s => s.MapFrom(x => x.Latitude))
                .ForMember(d => d.Longitude, s => s.MapFrom(x => x.Longitude));

            CreateMap<LocationDto, Location>()
                .IgnoreAllUnmapped()
                .ForMember(d => d.TaskId, s => s.MapFrom(x => x.TaskId))
                .ForMember(d => d.Latitude, s => s.MapFrom(x => x.Latitude))
                .ForMember(d => d.Longitude, s => s.MapFrom(x => x.Longitude))
                .ForMember(d => d.DateAdded, s => s.Ignore())
                .ForMember(d => d.DateModified, s => s.Ignore())
                .ForMember(d => d.Task, s => s.Ignore());

            CreateMap<Task, TaskDto>()
                .IgnoreAllUnmapped()
                .ForMember(d => d.UserId, s => s.MapFrom(x => x.UserId))
                .ForMember(d => d.Location, s => s.MapFrom(x => x.Location))
                .ForMember(d => d.IsCompleted, s => s.MapFrom(x => x.IsCompleted))
                .ForMember(d => d.Message, s => s.MapFrom(x => x.Message))
                .ForMember(d => d.TaskId, s => s.MapFrom(x => x.TaskId));

            CreateMap<TaskDto, Task>()
                .IgnoreAllUnmapped()
                .ForMember(d => d.UserId, s => s.MapFrom(x => x.UserId))
                .ForMember(d => d.Location, s => s.MapFrom(x => x.Location))
                .ForMember(d => d.IsCompleted, s => s.MapFrom(x => x.IsCompleted))
                .ForMember(d => d.Message, s => s.MapFrom(x => x.Message))
                .ForMember(d => d.TaskId, s => s.MapFrom(x => x.TaskId))
                .ForMember(d => d.DateAdded, s => s.Ignore())
                .ForMember(d => d.DateModified, s => s.Ignore());
        }
    }
}
