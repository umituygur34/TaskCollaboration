using TaskCollaboration.Api.DTOs.WorkTaskDto;



namespace TaskCollaboration.Api.Interfaces
{
    public interface IWorkTaskService
    {
        Task<List<WorkTaskDto>> GetAllTaskAsync(int userId);
        Task<WorkTaskDto?> GetTaskByIdAsync(int id, int userId);
        Task<WorkTaskDto> CreateTaskAsync(CreateWorkTaskDto createWorkTaskDto, int userId);
        Task<WorkTaskDto?> UpdateTaskAsync(int id, UpdateWorkTaskDto updateWorkTaskDto, int userId);
        Task<bool> DeleteTaskAsync(int id, int userId);
    }
}