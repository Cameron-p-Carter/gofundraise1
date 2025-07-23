using Microsoft.AspNetCore.Mvc;
using gofundraise3.Models.DTOs;
using gofundraise3.Services.Interfaces;

namespace gofundraise3.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Produces("application/json")]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TasksController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        /// <summary>
        /// Get all tasks
        /// </summary>
        /// <returns>List of all tasks</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllTasks()
        {
            var response = await _taskService.GetAllTasksAsync();
            
            if (response.Success)
                return Ok(response);
            
            return BadRequest(response);
        }

        /// <summary>
        /// Get a specific task by ID
        /// </summary>
        /// <param name="id">Task ID</param>
        /// <returns>Task details</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTaskById(int id)
        {
            var response = await _taskService.GetTaskByIdAsync(id);
            
            if (response.Success)
                return Ok(response);
            
            return NotFound(response);
        }

        /// <summary>
        /// Create a new task
        /// </summary>
        /// <param name="createTaskDto">Task creation data</param>
        /// <returns>Created task</returns>
        [HttpPost]
        public async Task<IActionResult> CreateTask([FromBody] CreateTaskItemDto createTaskDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await _taskService.CreateTaskAsync(createTaskDto);
            
            if (response.Success)
                return CreatedAtAction(nameof(GetTaskById), new { id = response.Data!.Id }, response);
            
            return BadRequest(response);
        }

        /// <summary>
        /// Update an existing task
        /// </summary>
        /// <param name="id">Task ID</param>
        /// <param name="updateTaskDto">Task update data</param>
        /// <returns>Updated task</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(int id, [FromBody] UpdateTaskItemDto updateTaskDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await _taskService.UpdateTaskAsync(id, updateTaskDto);
            
            if (response.Success)
                return Ok(response);
            
            return NotFound(response);
        }

        /// <summary>
        /// Delete a task
        /// </summary>
        /// <param name="id">Task ID</param>
        /// <returns>Deletion result</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var response = await _taskService.DeleteTaskAsync(id);
            
            if (response.Success)
                return Ok(response);
            
            return NotFound(response);
        }

        /// <summary>
        /// Get tasks by project ID
        /// </summary>
        /// <param name="projectId">Project ID</param>
        /// <returns>List of tasks for the specified project</returns>
        [HttpGet("project/{projectId}")]
        public async Task<IActionResult> GetTasksByProjectId(int projectId)
        {
            var response = await _taskService.GetTasksByProjectIdAsync(projectId);
            
            if (response.Success)
                return Ok(response);
            
            return NotFound(response);
        }

        /// <summary>
        /// Get tasks by status
        /// </summary>
        /// <param name="status">Task status (Todo, InProgress, Done, Cancelled)</param>
        /// <returns>List of tasks with the specified status</returns>
        [HttpGet("status/{status}")]
        public async Task<IActionResult> GetTasksByStatus(string status)
        {
            var response = await _taskService.GetTasksByStatusAsync(status);
            
            if (response.Success)
                return Ok(response);
            
            return BadRequest(response);
        }

        /// <summary>
        /// Get tasks by priority
        /// </summary>
        /// <param name="priority">Task priority (Low, Medium, High, Critical)</param>
        /// <returns>List of tasks with the specified priority</returns>
        [HttpGet("priority/{priority}")]
        public async Task<IActionResult> GetTasksByPriority(string priority)
        {
            var response = await _taskService.GetTasksByPriorityAsync(priority);
            
            if (response.Success)
                return Ok(response);
            
            return BadRequest(response);
        }
    }
}
