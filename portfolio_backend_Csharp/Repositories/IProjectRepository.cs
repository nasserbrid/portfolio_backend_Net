using portfolio_backend_Csharp.Models;

namespace portfolio_backend_Csharp.Repositories
{
    public interface IProjectRepository
    {
        Task<IEnumerable<Project>> GetAllProjects();
        Task<Project> GetProjectById(int projectId);

        Task<Project> CreateProject(Project project);

        Task<Project> UpdateProject(Project project);

        Task<bool> DeleteProjectById(int projectId);
    }
}
