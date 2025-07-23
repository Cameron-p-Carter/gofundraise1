using Microsoft.EntityFrameworkCore;
using gofundraise3.Data;
using gofundraise3.Entities;
using gofundraise3.Repositories.Interfaces;

namespace gofundraise3.Repositories.Implementations
{
    public class EfProjectRepository : IProjectRepository
    {
        private readonly ApplicationDbContext _context;

        public EfProjectRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Project>> GetAllAsync()
        {
            return await _context.Projects
                .Include(p => p.Tasks)
                .OrderBy(p => p.Name)
                .ToListAsync();
        }

        public async Task<Project?> GetByIdAsync(int id)
        {
            return await _context.Projects
                .Include(p => p.Tasks)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Project> CreateAsync(Project project)
        {
            project.CreatedDate = DateTime.UtcNow;
            project.UpdatedDate = DateTime.UtcNow;
            
            _context.Projects.Add(project);
            await _context.SaveChangesAsync();
            return project;
        }

        public async Task<Project> UpdateAsync(Project project)
        {
            project.UpdatedDate = DateTime.UtcNow;
            
            _context.Projects.Update(project);
            await _context.SaveChangesAsync();
            return project;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var project = await _context.Projects.FindAsync(id);
            if (project == null) return false;

            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Project>> GetByStatusAsync(ProjectStatus status)
        {
            return await _context.Projects
                .Include(p => p.Tasks)
                .Where(p => p.Status == status)
                .OrderBy(p => p.Name)
                .ToListAsync();
        }
    }
}
