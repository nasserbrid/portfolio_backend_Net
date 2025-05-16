using portfolio_backend_Csharp.Models;
using portfolio_backend_Csharp.Repositories;

namespace portfolio_backend_Csharp.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;
        private readonly ICloudinaryService _cloudinaryService;

        public ProjectService(IProjectRepository projectRepository, ICloudinaryService cloudinaryService)
        {
            _projectRepository = projectRepository;
             _cloudinaryService = cloudinaryService;
        }

        public async Task<Project> CreateProject(Project project, IFormFile? imageFile)
        {
            if (project == null) 
            {
                throw new ArgumentNullException(nameof(project));
            }

            if (imageFile != null)
            {
                var imageUrl = await _cloudinaryService.UploadImageAsync(imageFile);
                project.ImageUrl = imageUrl;
            }

            var createdProject = await _projectRepository.CreateProject(project);
            return createdProject;
        }

        public async Task<bool> DeleteProjectById(int projectId)
        {
            return await _projectRepository.DeleteProjectById(projectId);
        }

        public async Task<IEnumerable<Project>> GetAllProjects()
        {
            return await _projectRepository.GetAllProjects();
        }

        public async Task<Project> GetProjectById(int projectId)
        {
            return await _projectRepository.GetProjectById(projectId);
        }

        public async Task<Project> UpdateProject(Project project)
        {
            return await _projectRepository.UpdateProject(project);
        }
    }
}
