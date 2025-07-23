using gofundraise3.Models.DTOs;
using gofundraise3.Models.Common;

namespace gofundraise3.Services.Interfaces
{
    public interface ITaskService
    {
        Task<ApiResponse<IEnumerable<TaskItemDto>>> GetAllTasksAsync();
        Task<ApiResponse<TaskItemDto>> GetTaskByIdAsync(int id);
        Task<ApiResponse<IEnumerable<TaskItemDto>>> GetTasksByProjectIdAsync(int projectId);
        Task<ApiResponse<TaskItemDto>> CreateTaskAsync(CreateTaskItemDto createTaskDto);
        Task<ApiResponse<TaskItemDto>> UpdateTaskAsync(int id, UpdateTaskItemDto updateTaskDto);
        Task<ApiResponse> DeleteTaskAsync(int id);
        Task<ApiResponse<IEnumerable<TaskItemDto>>> GetTasksByStatusAsync(string status);
        Task<ApiResponse<IEnumerable<TaskItemDto>>> GetTasksByPriorityAsync(string priority);
    }
}
