using gofundraise3.Entities;

namespace gofundraise3.Repositories.Interfaces
{
    public interface ITaskRepository
    {
        Task<IEnumerable<TaskItem>> GetAllAsync();
        Task<TaskItem?> GetByIdAsync(int id);
        Task<IEnumerable<TaskItem>> GetByProjectIdAsync(int projectId);
        Task<TaskItem> CreateAsync(TaskItem task);
        Task<TaskItem> UpdateAsync(TaskItem task);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<TaskItem>> GetByStatusAsync(Entities.TaskStatus status);
        Task<IEnumerable<TaskItem>> GetByPriorityAsync(TaskPriority priority);
    }
}
