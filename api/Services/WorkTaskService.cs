using Microsoft.EntityFrameworkCore;
using TaskCollaboration.Api.Data;
using TaskCollaboration.Api.DTOs.WorkTaskDto;
using TaskCollaboration.Api.Mappers;
using TaskCollaboration.Api.Interfaces;
using TaskCollaboration.Api.Exceptions;





namespace TaskCollaboration.Api.Services
{
    public class WorkTaskService : IWorkTaskService
    {

        private readonly TaskCollaborationDbContext _context;

        public WorkTaskService(TaskCollaborationDbContext context)
        {
            _context = context;
        }

        public async Task<WorkTaskDto> CreateTaskAsync(CreateWorkTaskDto createWorkTaskDto, int userId)
        {

            var workTask = createWorkTaskDto.ToModel();

            workTask.UserId = userId;

            _context.Tasks.Add(workTask);

            await _context.SaveChangesAsync();

            return workTask.ToDto();
        }



        public async Task<List<WorkTaskDto>> GetAllTaskAsync(int userId)
        {
            return await _context.Tasks

            .Where(t => t.UserId == userId)

            .Select(t => t.ToDto())

            .ToListAsync();
        }

        public async Task<WorkTaskDto> GetTaskByIdAsync(int id, int userId)
        {
            var workTask = await _context.Tasks.FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);

            if (workTask == null)
            {
                return null;
            }
            return workTask.ToDto();

        }

        public async Task<WorkTaskDto> UpdateTaskAsync(int id, UpdateWorkTaskDto updateWorkTaskDto, int userId)
        {

            var workTask = await _context.Tasks.FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);


            if (workTask == null)
            {
                //Sadece task Not found dedik - 
                // yukarıda user kontrolü de yaptık düzenlenecek.

                throw new NotFoundException("Task not found");

            }


            workTask = workTask.UpdateToModel(updateWorkTaskDto);

            await _context.SaveChangesAsync();

            return workTask.ToDto();


        }
        public async Task<bool> DeleteTaskAsync(int id, int userId)
        {
            var existingTask = await _context.Tasks.FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);

            if (existingTask == null)
            {
                return false;

            }
            _context.Tasks.Remove(existingTask);
            await _context.SaveChangesAsync();
            return true;

        }
    }
}