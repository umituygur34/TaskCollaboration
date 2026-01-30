using TaskCollaboration.Api.DTOs.CommentDto;
using TaskCollaboration.Api.Models;

namespace TaskCollaboration.Api.Mappers;


public static class CommentMapper
{
    public static Comment ToModel(this CreateCommentDto createCommentDto)
    {
        return new Comment
        {
            Content = createCommentDto.Content,
            CreatedAt = DateTime.UtcNow,

        };

    }

    public static CommentDto ToDto(this Comment comment, UserInCommentDto createdByDto)
    {
        return new CommentDto
        {
            Id = comment.Id,
            Content = comment.Content,
            CreatedAt = comment.CreatedAt,
            CreatedBy = createdByDto

        };
    }


}