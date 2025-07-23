using AutoMapper;
using gofundraise3.Entities;
using gofundraise3.Models.Common;
using gofundraise3.Models.DTOs;
using gofundraise3.Repositories.Interfaces;
using gofundraise3.Services.Interfaces;

namespace gofundraise3.Services.Implementations
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IMapper _mapper;

        public TaskService(ITaskRepository taskRepository, IProjectRepository projectRepository, IMapper mapper)
        {
            _taskRepository = taskRepository;
            _projectRepository = projectRepository;
            _mapper = mapper;
        }

        public async Task<ApiResponse<IEnumerable<TaskItemDto>>> GetAllTasksAsync()
        {
            try
            {
                var tasks = await _taskRepository.GetAllAsync();
                var taskDtos = _mapper.Map<IEnumerable<TaskItemDto>>(tasks);
                return ApiResponse<IEnumerable<TaskItemDto>>.SuccessResponse(taskDtos, "Tasks retrieved successfully");
            }
            catch (Exception ex)
            {
                return ApiResponse<IEnumerable<TaskItemDto>>.ErrorResponse("Failed to retrieve tasks", ex.Message);
            }
        }

        public async Task<ApiResponse<TaskItemDto>> GetTaskByIdAsync(int id)
        {
            try
            {
                var task = await _taskRepository.GetByIdAsync(id);
                if (task == null)
                {
                    return ApiResponse<TaskItemDto>.ErrorResponse("Task not found", $"Task with ID {id} does not exist");
                }

                var taskDto = _mapper.Map<TaskItemDto>(task);
                return ApiResponse<TaskItemDto>.SuccessResponse(taskDto, "Task retrieved successfully");
            }
            catch (Exception ex)
            {
                return ApiResponse<TaskItemDto>.ErrorResponse("Failed to retrieve task", ex.Message);
            }
        }

        public async Task<ApiResponse<IEnumerable<TaskItemDto>>> GetTasksByProjectIdAsync(int projectId)
        {
            try
            {
                // Verify project exists
                var project = await _projectRepository.GetByIdAsync(projectId);
                if (project == null)
                {
                    return ApiResponse<IEnumerable<TaskItemDto>>.ErrorResponse("Project not found", $"Project with ID {projectId} does not exist");
                }

                var tasks = await _taskRepository.GetByProjectIdAsync(projectId);
                var taskDtos = _mapper.Map<IEnumerable<TaskItemDto>>(tasks);
                return ApiResponse<IEnumerable<TaskItemDto>>.SuccessResponse(taskDtos, $"Tasks for project '{project.Name}' retrieved successfully");
            }
            catch (Exception ex)
            {
                return ApiResponse<IEnumerable<TaskItemDto>>.ErrorResponse("Failed to retrieve tasks by project", ex.Message);
            }
        }

        public async Task<ApiResponse<TaskItemDto>> CreateTaskAsync(CreateTaskItemDto createTaskDto)
        {
            try
            {
                // Verify project exists
                var project = await _projectRepository.GetByIdAsync(createTaskDto.ProjectId);
                if (project == null)
                {
                    return ApiResponse<TaskItemDto>.ErrorResponse("Project not found", $"Project with ID {createTaskDto.ProjectId} does not exist");
                }

                // Validate status
                if (!Enum.TryParse<Entities.TaskStatus>(createTaskDto.Status, out _))
                {
                    return ApiResponse<TaskItemDto>.ErrorResponse("Invalid task status", 
                        $"Status must be one of: {string.Join(", ", Enum.GetNames<Entities.TaskStatus>())}");
                }

                // Validate priority
                if (!Enum.TryParse<TaskPriority>(createTaskDto.Priority, out _))
                {
                    return ApiResponse<TaskItemDto>.ErrorResponse("Invalid task priority", 
                        $"Priority must be one of: {string.Join(", ", Enum.GetNames<TaskPriority>())}");
                }

                var task = _mapper.Map<TaskItem>(createTaskDto);
                var createdTask = await _taskRepository.CreateAsync(task);
                var taskDto = _mapper.Map<TaskItemDto>(createdTask);

                return ApiResponse<TaskItemDto>.SuccessResponse(taskDto, "Task created successfully");
            }
            catch (Exception ex)
            {
                return ApiResponse<TaskItemDto>.ErrorResponse("Failed to create task", ex.Message);
            }
        }

        public async Task<ApiResponse<TaskItemDto>> UpdateTaskAsync(int id, UpdateTaskItemDto updateTaskDto)
        {
            try
            {
                var existingTask = await _taskRepository.GetByIdAsync(id);
                if (existingTask == null)
                {
                    return ApiResponse<TaskItemDto>.ErrorResponse("Task not found", $"Task with ID {id} does not exist");
                }

                // Validate status
                if (!Enum.TryParse<Entities.TaskStatus>(updateTaskDto.Status, out _))
                {
                    return ApiResponse<TaskItemDto>.ErrorResponse("Invalid task status", 
                        $"Status must be one of: {string.Join(", ", Enum.GetNames<Entities.TaskStatus>())}");
                }

                // Validate priority
                if (!Enum.TryParse<TaskPriority>(updateTaskDto.Priority, out _))
                {
                    return ApiResponse<TaskItemDto>.ErrorResponse("Invalid task priority", 
                        $"Priority must be one of: {string.Join(", ", Enum.GetNames<TaskPriority>())}");
                }

                // Map the update DTO to the existing task
                _mapper.Map(updateTaskDto, existingTask);
                existingTask.Id = id; // Ensure ID is preserved

                var updatedTask = await _taskRepository.UpdateAsync(existingTask);
                var taskDto = _mapper.Map<TaskItemDto>(updatedTask);

                return ApiResponse<TaskItemDto>.SuccessResponse(taskDto, "Task updated successfully");
            }
            catch (Exception ex)
            {
                return ApiResponse<TaskItemDto>.ErrorResponse("Failed to update task", ex.Message);
            }
        }

        public async Task<ApiResponse> DeleteTaskAsync(int id)
        {
            try
            {
                var success = await _taskRepository.DeleteAsync(id);
                if (!success)
                {
                    return ApiResponse.ErrorResponse("Task not found", $"Task with ID {id} does not exist");
                }

                return ApiResponse.SuccessResponse("Task deleted successfully");
            }
            catch (Exception ex)
            {
                return ApiResponse.ErrorResponse("Failed to delete task", ex.Message);
            }
        }

        public async Task<ApiResponse<IEnumerable<TaskItemDto>>> GetTasksByStatusAsync(string status)
        {
            try
            {
                if (!Enum.TryParse<Entities.TaskStatus>(status, out var taskStatus))
                {
                    return ApiResponse<IEnumerable<TaskItemDto>>.ErrorResponse("Invalid task status", 
                        $"Status must be one of: {string.Join(", ", Enum.GetNames<Entities.TaskStatus>())}");
                }

                var tasks = await _taskRepository.GetByStatusAsync(taskStatus);
                var taskDtos = _mapper.Map<IEnumerable<TaskItemDto>>(tasks);

                return ApiResponse<IEnumerable<TaskItemDto>>.SuccessResponse(taskDtos, $"Tasks with status '{status}' retrieved successfully");
            }
            catch (Exception ex)
            {
                return ApiResponse<IEnumerable<TaskItemDto>>.ErrorResponse("Failed to retrieve tasks by status", ex.Message);
            }
        }

        public async Task<ApiResponse<IEnumerable<TaskItemDto>>> GetTasksByPriorityAsync(string priority)
        {
            try
            {
                if (!Enum.TryParse<TaskPriority>(priority, out var taskPriority))
                {
                    return ApiResponse<IEnumerable<TaskItemDto>>.ErrorResponse("Invalid task priority", 
                        $"Priority must be one of: {string.Join(", ", Enum.GetNames<TaskPriority>())}");
                }

                var tasks = await _taskRepository.GetByPriorityAsync(taskPriority);
                var taskDtos = _mapper.Map<IEnumerable<TaskItemDto>>(tasks);

                return ApiResponse<IEnumerable<TaskItemDto>>.SuccessResponse(taskDtos, $"Tasks with priority '{priority}' retrieved successfully");
            }
            catch (Exception ex)
            {
                return ApiResponse<IEnumerable<TaskItemDto>>.ErrorResponse("Failed to retrieve tasks by priority", ex.Message);
            }
        }
    }
}
