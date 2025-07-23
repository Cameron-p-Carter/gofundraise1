using gofundraise3.Entities;

namespace gofundraise3.Repositories.Interfaces
{
    public interface IProjectRepository
    {
        Task<IEnumerable<Project>> GetAllAsync();
        Task<Project?> GetByIdAsync(int id);
        Task<Project> CreateAsync(Project project);
        Task<Project> UpdateAsync(Project project);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<Project>> GetByStatusAsync(ProjectStatus status);
    }
}
