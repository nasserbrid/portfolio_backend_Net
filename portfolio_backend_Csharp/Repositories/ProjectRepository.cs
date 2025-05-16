using Microsoft.EntityFrameworkCore;
using portfolio_backend_Csharp.Data;
using portfolio_backend_Csharp.Models;

namespace portfolio_backend_Csharp.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly ProjectBackendContext _projectBackendContext;

        public ProjectRepository(ProjectBackendContext projectBackendContext)
        {
            _projectBackendContext = projectBackendContext;
        }
        public async Task<Project> CreateProject(Project project)
        {
            var result = await _projectBackendContext.Projects.AddAsync(project);
            await _projectBackendContext.SaveChangesAsync();
            return result.Entity;
       
        }

        public async Task<bool> DeleteProjectById(int projectId)
        {
            var project = await _projectBackendContext.Projects
                .FindAsync(projectId);

            if (project == null)
            {
                return false;
            }

            _projectBackendContext.Projects .Remove(project);
            await _projectBackendContext.SaveChangesAsync();
            return true;
        }
       
        public async Task<IEnumerable<Project>> GetAllProjects()
        {
            return await _projectBackendContext.Projects.ToListAsync();
        }

        
        public async Task<Project> GetProjectById(int projectId)
        {
            return await _projectBackendContext.Projects.FindAsync(projectId);
        }

        public async Task<Project> UpdateProject(Project project)
        {            
            var existProject = await _projectBackendContext.Projects
                .FindAsync(project.Id);

            if (existProject != null) 
            {
                existProject.Title = project.Title;
                existProject.Description = project.Description;
                existProject.Skills = project.Skills;
                existProject.ImageUrl = project.ImageUrl;
                await _projectBackendContext.SaveChangesAsync();

                return existProject;
            }

            return null;
        }
    }
}
