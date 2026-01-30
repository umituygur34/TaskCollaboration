using TaskCollaboration.Api.DTOs.CommentDto;


namespace TaskCollaboration.Api.Interfaces;

public interface ICommentService


{

    Task<List<CommentDto>> GetCommentsByTaskIdAsync(int taskId);

    Task<CommentDto> GetCommentByIdAsync(int commentId);

    Task<CommentDto> CreateCommentAsync(int taskId, CreateCommentDto createCommentDto, int userId, string userName);
    Task<CommentDto> UpdateCommentAsync(int commentId, UpdateCommentDto updateCommentDto, int userId);
    Task DeleteCommentAsync(int commentId, int userId);



}
