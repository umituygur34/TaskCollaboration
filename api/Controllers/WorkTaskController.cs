using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskCollaboration.Api.Interfaces;
using TaskCollaboration.Api.DTOs;
using TaskCollaboration.Api.DTOs.WorkTaskDto;
using TaskCollaboration.Api.Middleware;


namespace TaskCollaboration.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]

public class WorkTaskController : ControllerBase
{

    private readonly IWorkTaskService _workTaskService;

    public WorkTaskController(IWorkTaskService workTaskService)
    {
        _workTaskService = workTaskService;
    }
    private int UserId => int.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier).Value);


    [HttpGet]
    public async Task<IActionResult> GetAllTasks()
    {

        var tasks = await _workTaskService.GetAllTaskAsync(UserId);
        if (tasks == null || tasks.Count == 0)
        {
            return NotFound("No tasks found for the user.");
        }
        return Ok(tasks);

    }
    [HttpPost]
    public async Task<IActionResult> CreateTask([FromBody] CreateWorkTaskDto createWorkTaskDto)
    {
        var createdTask = await _workTaskService.CreateTaskAsync(createWorkTaskDto, UserId);
        return Ok(createdTask);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetTaskById(int id)
    {
        var task = await _workTaskService.GetTaskByIdAsync(id, UserId);
        if (task == null)
        {
            return NotFound("Task not found");
        }
        return Ok(task);
    }
    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateTask(int id, [FromBody] UpdateWorkTaskDto updateWorkTaskDto)
    {
        var updateTask = await _workTaskService.UpdateTaskAsync(id, updateWorkTaskDto, UserId);
        if (updateTask == null)
        {
            return NotFound("Task not found");
        }
        return Ok(updateTask);
    }
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteTask(int id)
    {
        var deleteTask = await _workTaskService.DeleteTaskAsync(id, UserId);
        if (!deleteTask)
        {
            return NotFound("Task not found");
        }
        return Ok("Task deleted successfully");
    }



}