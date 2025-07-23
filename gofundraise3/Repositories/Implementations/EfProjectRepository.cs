using Microsoft.EntityFrameworkCore;
using gofundraise3.Data;
using gofundraise3.Entities;
using gofundraise3.Repositories.Interfaces;

namespace gofundraise3.Repositories.Implementations
{
    public class EfProjectRepository : EfRepository<Project>, IProjectRepository
    {
        public EfProjectRepository(ApplicationDbContext context) : base(context)
        {
        }

        public override async Task<IEnumerable<Project>> GetAllAsync()
        {
            return await _context.Projects
                .Include(p => p.Tasks)
                .OrderBy(p => p.Name)
                .ToListAsync();
        }

        public override async Task<Project?> GetByIdAsync(int id)
        {
            return await _context.Projects
                .Include(p => p.Tasks)
                .FirstOrDefaultAsync(p => p.Id == id);
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
