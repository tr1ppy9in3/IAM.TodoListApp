using AutoMapper;
using IAD.TodoListApp.Contracts;
using IAD.TodoListApp.Core;
using IAD.TodoListApp.UseCases.Commands.Task;

namespace IAD.TodoListApp.UseCases;
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<TaskInputModel, TodoTask>()
            .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.CategoryId ?? 0));

        CreateMap<TodoTask, TaskModel>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.DueDate, opt => opt.MapFrom(src => src.DueDate))
            .ForMember(dest => dest.Priority, opt => opt.MapFrom(src => src.Priority))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
            .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.CategoryId))
            .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category != null ? src.Category.Name : null));
    }
}
