using gofundraise3.Models.DTOs;
using gofundraise3.Models.Common;
using gofundraise3.Entities;

namespace gofundraise3.Services.Interfaces
{
    public interface IProjectService
    {
        Task<ApiResponse<IEnumerable<ProjectDto>>> GetAllProjectsAsync();
        Task<ApiResponse<ProjectDto>> GetProjectByIdAsync(int id);
        Task<ApiResponse<ProjectDto>> CreateProjectAsync(CreateProjectDto createProjectDto);
        Task<ApiResponse<ProjectDto>> UpdateProjectAsync(int id, UpdateProjectDto updateProjectDto);
        Task<ApiResponse> DeleteProjectAsync(int id);
        Task<ApiResponse<IEnumerable<ProjectDto>>> GetProjectsByStatusAsync(string status);
    }
}
