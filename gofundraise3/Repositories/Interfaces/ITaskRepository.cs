using gofundraise3.Entities;

namespace gofundraise3.Repositories.Interfaces
{
    public interface ITaskRepository : IRepository<TaskItem>
    {
        Task<IEnumerable<TaskItem>> GetByProjectIdAsync(int projectId);
        Task<IEnumerable<TaskItem>> GetByStatusAsync(Entities.TaskStatus status);
        Task<IEnumerable<TaskItem>> GetByPriorityAsync(TaskPriority priority);
    }
}
