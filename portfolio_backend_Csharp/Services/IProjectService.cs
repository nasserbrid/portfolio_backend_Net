using portfolio_backend_Csharp.Models;

namespace portfolio_backend_Csharp.Services
{
    public interface IProjectService
    {
        Task<IEnumerable<Project>> GetAllProjects();
        Task<Project> GetProjectById(int projectId);

        Task<Project> CreateProject(Project project, IFormFile? imageFile);

        Task<Project> UpdateProject(Project project);

        Task<bool> DeleteProjectById(int projectId);
    }
}
