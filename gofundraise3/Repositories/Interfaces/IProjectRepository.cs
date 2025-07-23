using gofundraise3.Entities;

namespace gofundraise3.Repositories.Interfaces
{
    public interface IProjectRepository : IRepository<Project>
    {
        Task<IEnumerable<Project>> GetByStatusAsync(ProjectStatus status);
    }
}
