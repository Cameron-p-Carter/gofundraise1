using Microsoft.AspNetCore.Mvc;
using gofundraise3.Models.DTOs;
using gofundraise3.Services.Interfaces;

namespace gofundraise3.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Produces("application/json")]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectService _projectService;

        public ProjectsController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        /// <summary>
        /// Get all projects
        /// </summary>
        /// <returns>List of all projects</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllProjects()
        {
            var response = await _projectService.GetAllProjectsAsync();
            
            if (response.Success)
                return Ok(response);
            
            return BadRequest(response);
        }

        /// <summary>
        /// Get a specific project by ID
        /// </summary>
        /// <param name="id">Project ID</param>
        /// <returns>Project details</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProjectById(int id)
        {
            var response = await _projectService.GetProjectByIdAsync(id);
            
            if (response.Success)
                return Ok(response);
            
            return NotFound(response);
        }

        /// <summary>
        /// Create a new project
        /// </summary>
        /// <param name="createProjectDto">Project creation data</param>
        /// <returns>Created project</returns>
        [HttpPost]
        public async Task<IActionResult> CreateProject([FromBody] CreateProjectDto createProjectDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await _projectService.CreateProjectAsync(createProjectDto);
            
            if (response.Success)
                return CreatedAtAction(nameof(GetProjectById), new { id = response.Data!.Id }, response);
            
            return BadRequest(response);
        }

        /// <summary>
        /// Update an existing project
        /// </summary>
        /// <param name="id">Project ID</param>
        /// <param name="updateProjectDto">Project update data</param>
        /// <returns>Updated project</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProject(int id, [FromBody] UpdateProjectDto updateProjectDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await _projectService.UpdateProjectAsync(id, updateProjectDto);
            
            if (response.Success)
                return Ok(response);
            
            return NotFound(response);
        }

        /// <summary>
        /// Delete a project
        /// </summary>
        /// <param name="id">Project ID</param>
        /// <returns>Deletion result</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject(int id)
        {
            var response = await _projectService.DeleteProjectAsync(id);
            
            if (response.Success)
                return Ok(response);
            
            return NotFound(response);
        }

        /// <summary>
        /// Get projects by status
        /// </summary>
        /// <param name="status">Project status (Planning, Active, OnHold, Completed, Cancelled)</param>
        /// <returns>List of projects with the specified status</returns>
        [HttpGet("status/{status}")]
        public async Task<IActionResult> GetProjectsByStatus(string status)
        {
            var response = await _projectService.GetProjectsByStatusAsync(status);
            
            if (response.Success)
                return Ok(response);
            
            return BadRequest(response);
        }
    }
}
