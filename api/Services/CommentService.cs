using Microsoft.EntityFrameworkCore;
using TaskCollaboration.Api.Data;
using TaskCollaboration.Api.DTOs.CommentDto;
using TaskCollaboration.Api.Exceptions;
using TaskCollaboration.Api.Interfaces;
using TaskCollaboration.Api.Mappers;
using TaskCollaboration.Api.Models;

namespace TaskCollaboration.Api.Services;

public class CommentService : ICommentService
{
    //DbContext DI eklendi.
    private readonly TaskCollaborationDbContext _context;

    public CommentService(TaskCollaborationDbContext context)
    {
        _context = context;
    }

    //CreateComment
    public async Task<CommentDto> CreateCommentAsync(int taskId, CreateCommentDto createCommentDto, int userId, string userName)
    {
        var existingTask = await _context.Tasks.FindAsync(taskId);
        if (existingTask == null)
        {
            throw new NotFoundException($"Task with ID {taskId} not found.");
        }

        var comment = createCommentDto.ToModel();
        comment.UserId = userId;
        comment.WorkTaskId = taskId;

        _context.Comments.Add(comment);
        await _context.SaveChangesAsync();

        var createdUserInCommentDto = new UserInCommentDto
        {
            Id = userId,
            Name = userName
        };

        return comment.ToDto(createdUserInCommentDto);
    }




    //GetCommentById
    public async Task<CommentDto> GetCommentByIdAsync(int commentId)
    {
        var comment = await _context.Comments
        .Include(c => c.User)
        .FirstOrDefaultAsync(c => c.Id == commentId) ?? throw new NotFoundException("Comment not found");

        var createdByDto = new UserInCommentDto
        {
            Id = comment.User.Id,
            Name = comment.User.Name
        };
        return comment.ToDto(createdByDto);
    }


    //GetCommentsByTaskId
    public async Task<List<CommentDto>> GetCommentsByTaskIdAsync(int taskId)
    {
        return await _context.Comments
            .Where(c => c.WorkTaskId == taskId)
            .Select(c => c.ToDto(new UserInCommentDto
            {
                Id = c.User.Id,
                Name = c.User.Name
            }))
            .ToListAsync();
    }
    //DeleteComment

    public async Task DeleteCommentAsync(int commentId, int userId)
    {
        var comment = await _context.Comments.FirstOrDefaultAsync(c => c.Id == commentId && c.UserId == userId);
        if (comment == null)
        {
            throw new NotFoundException("Comment not found or you do not have permission to delete this comment.");
        }
        _context.Comments.Remove(comment);
        await _context.SaveChangesAsync();

    }

    //UpdateComment
    public async Task<CommentDto> UpdateCommentAsync(int commentId, UpdateCommentDto updateCommentDto, int userId)
    {
        var comment = await _context.Comments
        .Include(c => c.User)
        .FirstOrDefaultAsync(c => c.Id == commentId && c.UserId == userId);
        if (comment == null)
        {
            throw new NotFoundException("Comment not found or you do not have permission to update this comment.");
        }


        comment.Content = updateCommentDto.Content;
        await _context.SaveChangesAsync();

        return comment.ToDto(new UserInCommentDto
        {
            Id = comment.User.Id,
            Name = comment.User.Name
        });
    }
}