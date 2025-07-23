using AutoMapper;
using gofundraise3.Entities;
using gofundraise3.Models.DTOs;

namespace gofundraise3.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Project mappings
            CreateMap<Project, ProjectDto>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));

            CreateMap<CreateProjectDto, Project>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => Enum.Parse<ProjectStatus>(src.Status)))
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedDate, opt => opt.Ignore())
                .ForMember(dest => dest.Tasks, opt => opt.Ignore());

            CreateMap<UpdateProjectDto, Project>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => Enum.Parse<ProjectStatus>(src.Status)))
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedDate, opt => opt.Ignore())
                .ForMember(dest => dest.Tasks, opt => opt.Ignore());

            // TaskItem mappings
            CreateMap<TaskItem, TaskItemDto>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
                .ForMember(dest => dest.Priority, opt => opt.MapFrom(src => src.Priority.ToString()))
                .ForMember(dest => dest.ProjectName, opt => opt.MapFrom(src => src.Project != null ? src.Project.Name : string.Empty));

            CreateMap<CreateTaskItemDto, TaskItem>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => Enum.Parse<Entities.TaskStatus>(src.Status)))
                .ForMember(dest => dest.Priority, opt => opt.MapFrom(src => Enum.Parse<TaskPriority>(src.Priority)))
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedDate, opt => opt.Ignore())
                .ForMember(dest => dest.Project, opt => opt.Ignore());

            CreateMap<UpdateTaskItemDto, TaskItem>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => Enum.Parse<Entities.TaskStatus>(src.Status)))
                .ForMember(dest => dest.Priority, opt => opt.MapFrom(src => Enum.Parse<TaskPriority>(src.Priority)))
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.ProjectId, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedDate, opt => opt.Ignore())
                .ForMember(dest => dest.Project, opt => opt.Ignore());
        }
    }
}
