using AutoMapper;
using gofundraise3.Entities;
using gofundraise3.Models.Common;
using gofundraise3.Models.DTOs;
using gofundraise3.Repositories.Interfaces;
using gofundraise3.Services.Interfaces;

namespace gofundraise3.Services.Implementations
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IMapper _mapper;

        public ProjectService(IProjectRepository projectRepository, IMapper mapper)
        {
            _projectRepository = projectRepository;
            _mapper = mapper;
        }

        public async Task<ApiResponse<IEnumerable<ProjectDto>>> GetAllProjectsAsync()
        {
            try
            {
                var projects = await _projectRepository.GetAllAsync();
                var projectDtos = _mapper.Map<IEnumerable<ProjectDto>>(projects);
                return ApiResponse<IEnumerable<ProjectDto>>.SuccessResponse(projectDtos, "Projects retrieved successfully");
            }
            catch (Exception ex)
            {
                return ApiResponse<IEnumerable<ProjectDto>>.ErrorResponse("Failed to retrieve projects", ex.Message);
            }
        }

        public async Task<ApiResponse<ProjectDto>> GetProjectByIdAsync(int id)
        {
            try
            {
                var project = await _projectRepository.GetByIdAsync(id);
                if (project == null)
                {
                    return ApiResponse<ProjectDto>.ErrorResponse("Project not found", $"Project with ID {id} does not exist");
                }

                var projectDto = _mapper.Map<ProjectDto>(project);
                return ApiResponse<ProjectDto>.SuccessResponse(projectDto, "Project retrieved successfully");
            }
            catch (Exception ex)
            {
                return ApiResponse<ProjectDto>.ErrorResponse("Failed to retrieve project", ex.Message);
            }
        }

        public async Task<ApiResponse<ProjectDto>> CreateProjectAsync(CreateProjectDto createProjectDto)
        {
            try
            {
                // Validate status
                if (!Enum.TryParse<ProjectStatus>(createProjectDto.Status, out _))
                {
                    return ApiResponse<ProjectDto>.ErrorResponse("Invalid project status", 
                        $"Status must be one of: {string.Join(", ", Enum.GetNames<ProjectStatus>())}");
                }

                var project = _mapper.Map<Project>(createProjectDto);
                var createdProject = await _projectRepository.CreateAsync(project);
                var projectDto = _mapper.Map<ProjectDto>(createdProject);

                return ApiResponse<ProjectDto>.SuccessResponse(projectDto, "Project created successfully");
            }
            catch (Exception ex)
            {
                return ApiResponse<ProjectDto>.ErrorResponse("Failed to create project", ex.Message);
            }
        }

        public async Task<ApiResponse<ProjectDto>> UpdateProjectAsync(int id, UpdateProjectDto updateProjectDto)
        {
            try
            {
                var existingProject = await _projectRepository.GetByIdAsync(id);
                if (existingProject == null)
                {
                    return ApiResponse<ProjectDto>.ErrorResponse("Project not found", $"Project with ID {id} does not exist");
                }

                // Validate status
                if (!Enum.TryParse<ProjectStatus>(updateProjectDto.Status, out _))
                {
                    return ApiResponse<ProjectDto>.ErrorResponse("Invalid project status", 
                        $"Status must be one of: {string.Join(", ", Enum.GetNames<ProjectStatus>())}");
                }

                // Map the update DTO to the existing project
                _mapper.Map(updateProjectDto, existingProject);
                existingProject.Id = id; // Ensure ID is preserved

                var updatedProject = await _projectRepository.UpdateAsync(existingProject);
                var projectDto = _mapper.Map<ProjectDto>(updatedProject);

                return ApiResponse<ProjectDto>.SuccessResponse(projectDto, "Project updated successfully");
            }
            catch (Exception ex)
            {
                return ApiResponse<ProjectDto>.ErrorResponse("Failed to update project", ex.Message);
            }
        }

        public async Task<ApiResponse> DeleteProjectAsync(int id)
        {
            try
            {
                var success = await _projectRepository.DeleteAsync(id);
                if (!success)
                {
                    return ApiResponse.ErrorResponse("Project not found", $"Project with ID {id} does not exist");
                }

                return ApiResponse.SuccessResponse("Project deleted successfully");
            }
            catch (Exception ex)
            {
                return ApiResponse.ErrorResponse("Failed to delete project", ex.Message);
            }
        }

        public async Task<ApiResponse<IEnumerable<ProjectDto>>> GetProjectsByStatusAsync(string status)
        {
            try
            {
                if (!Enum.TryParse<ProjectStatus>(status, out var projectStatus))
                {
                    return ApiResponse<IEnumerable<ProjectDto>>.ErrorResponse("Invalid project status", 
                        $"Status must be one of: {string.Join(", ", Enum.GetNames<ProjectStatus>())}");
                }

                var projects = await _projectRepository.GetByStatusAsync(projectStatus);
                var projectDtos = _mapper.Map<IEnumerable<ProjectDto>>(projects);

                return ApiResponse<IEnumerable<ProjectDto>>.SuccessResponse(projectDtos, $"Projects with status '{status}' retrieved successfully");
            }
            catch (Exception ex)
            {
                return ApiResponse<IEnumerable<ProjectDto>>.ErrorResponse("Failed to retrieve projects by status", ex.Message);
            }
        }
    }
}
