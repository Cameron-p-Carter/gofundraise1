using Microsoft.EntityFrameworkCore;
using gofundraise3.Data;
using gofundraise3.Entities;
using gofundraise3.Repositories.Interfaces;

namespace gofundraise3.Repositories.Implementations
{
    public class EfTaskRepository : ITaskRepository
    {
        private readonly ApplicationDbContext _context;

        public EfTaskRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TaskItem>> GetAllAsync()
        {
            return await _context.Tasks
                .Include(t => t.Project)
                .OrderBy(t => t.CreatedDate)
                .ToListAsync();
        }

        public async Task<TaskItem?> GetByIdAsync(int id)
        {
            return await _context.Tasks
                .Include(t => t.Project)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<IEnumerable<TaskItem>> GetByProjectIdAsync(int projectId)
        {
            return await _context.Tasks
                .Include(t => t.Project)
                .Where(t => t.ProjectId == projectId)
                .OrderBy(t => t.CreatedDate)
                .ToListAsync();
        }

        public async Task<TaskItem> CreateAsync(TaskItem task)
        {
            task.CreatedDate = DateTime.UtcNow;
            task.UpdatedDate = DateTime.UtcNow;
            
            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();
            
            // Return the task with the project included
            return await GetByIdAsync(task.Id) ?? task;
        }

        public async Task<TaskItem> UpdateAsync(TaskItem task)
        {
            task.UpdatedDate = DateTime.UtcNow;
            
            _context.Tasks.Update(task);
            await _context.SaveChangesAsync();
            
            // Return the task with the project included
            return await GetByIdAsync(task.Id) ?? task;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null) return false;

            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<TaskItem>> GetByStatusAsync(Entities.TaskStatus status)
        {
            return await _context.Tasks
                .Include(t => t.Project)
                .Where(t => t.Status == status)
                .OrderBy(t => t.CreatedDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<TaskItem>> GetByPriorityAsync(TaskPriority priority)
        {
            return await _context.Tasks
                .Include(t => t.Project)
                .Where(t => t.Priority == priority)
                .OrderBy(t => t.CreatedDate)
                .ToListAsync();
        }
    }
}
