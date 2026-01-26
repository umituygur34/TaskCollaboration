using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskCollaboration.Api.api.DTOs;

namespace TaskCollaboration.Api.api.Interfaces
{
    public interface IWorkTaskService
    {
        Task<List<WorkTaskDto>> GetAllTaskAsync(int userId);
        Task<WorkTaskDto> GetTaskByIdAsync(int id, int userId);
        Task<WorkTaskDto> CreateTaskAsync(WorkTaskCreateDto workTaskCreateDto, int userId);
        Task<WorkTaskDto> UpdateTaskAsync(int id, WorkTaskCreateDto workTaskUpdateDto, int userId);
        Task<bool> DeleteTaskAsync(int id, int userId);



    }
}