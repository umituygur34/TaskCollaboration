using System;
using TaskCollaboration.Api.DTOs.CommentDto;

namespace TaskCollaboration.Api.DTOs.CommentDto;

public class CommentDto
{

    public int Id { get; set; }
    public string Content { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public UserInCommentDto CreatedBy { get; set; }



}