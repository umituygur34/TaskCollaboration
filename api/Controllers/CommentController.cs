using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TaskCollaboration.Api.DTOs.CommentDto;
using TaskCollaboration.Api.Interfaces;

namespace TaskCollaboration.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class CommentController(ICommentService commentService) : ControllerBase
{
    private readonly ICommentService _commentService = commentService;

    private int UserId => int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

    [HttpPost("task/{taskId}")]
    public async Task<IActionResult> CreateComment(int taskId, [FromBody] CreateCommentDto createCommentDto)
    {
        var userName = User.FindFirst(ClaimTypes.Name)?.Value ?? "Unknown";

        var createdComment = await _commentService.CreateCommentAsync(taskId, createCommentDto, UserId, userName);

        return Ok(createdComment);
    }

    [HttpGet("task/{taskId}")]
    public async Task<IActionResult> GetCommentsByTaskId(int taskId)
    {
        var comments = await _commentService.GetCommentsByTaskIdAsync(taskId);

        if (comments == null || comments.Count == 0)
        {
            return NotFound("No comments found for the task.");
        }

        return Ok(comments);
    }

    [HttpGet("{commentId:int}")]
    public async Task<IActionResult> GetCommentById(int commentId)
    {
        var comment = await _commentService.GetCommentByIdAsync(commentId);


        return Ok(comment);

    }

    //DeleteComment
    [HttpDelete("{commentId:int}")]
    public async Task<IActionResult> DeleteComment(int commentId)
    {
        await _commentService.DeleteCommentAsync(commentId, UserId);

        return NoContent();

    }






}