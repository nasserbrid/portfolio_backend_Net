using Microsoft.AspNetCore.Mvc;
using portfolio_backend_Csharp.Models;
using portfolio_backend_Csharp.Services;

namespace portfolio_backend_Csharp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _projectService;

        public ProjectController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        // GET: api/Project
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Project>>> GetAllProjects()
        {
            var projects = await _projectService.GetAllProjects();
            return Ok(projects);
        }

        // GET: api/Project/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Project>> GetProjectById(int id)
        {
            var project = await _projectService.GetProjectById(id);
            if (project == null)
            {
                return NotFound();
            }
            return Ok(project);
        }

        // POST: api/Project
        [HttpPost]
        public async Task<ActionResult<Project>> CreateProject([FromForm] Project project,IFormFile? imageFile)
        {
            var createdProject = await _projectService.CreateProject(project, imageFile);
            return CreatedAtAction(nameof(GetProjectById), new { id = createdProject.Id }, createdProject);
        }

        // PUT: api/Project/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<Project>> UpdateProject(int id, [FromBody] Project project)
        {
            if (id != project.Id)
            {
                return BadRequest("Project ID mismatch");
            }

            var updatedProject = await _projectService.UpdateProject(project);
            if (updatedProject == null)
            {
                return NotFound();
            }

            return Ok(updatedProject);
        }

        // DELETE: api/Project/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject(int id)
        {
            var success = await _projectService.DeleteProjectById(id);
            if (!success)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
