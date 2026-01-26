using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TaskCollaboration.Api.api.Data;
using TaskCollaboration.Api.api.DTOs;
using TaskCollaboration.Api.api.Interfaces;
using TaskCollaboration.Api.api.Mappers;
using TaskCollaboration.Api.api.Models.Enums;

namespace TaskCollaboration.Api.api.Services
{
    public class WorkTaskService : IWorkTaskService
    {

        private readonly TaskCollaborationDbContext _context;

        public WorkTaskService(TaskCollaborationDbContext context)
        {
            _context = context;
        }

        public async Task<WorkTaskDto> CreateTaskAsync(WorkTaskCreateDto workTaskCreateDto, int userId)
        {

            var workTask = workTaskCreateDto.ToModel(userId);

            _context.Tasks.Add(workTask);

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

        public async Task<WorkTaskDto> UpdateTaskAsync(int id, WorkTaskCreateDto workTaskUpdateDto, int userId)
        {

            var workTask = await _context.Tasks.FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);

            if (workTask == null)
            {
                return null;
            }

            workTask.Title = workTaskUpdateDto.Title;
            workTask.Description = workTaskUpdateDto.Description;
            workTask.Status = Enum.Parse<WorkTaskStatus>(workTaskUpdateDto.Status);
            workTask.Priority = Enum.Parse<WorkTaskPriority>(workTaskUpdateDto.Priority);
            workTask.DueDate = workTaskUpdateDto.DueDate;

            await _context.SaveChangesAsync();

            return workTask.ToDto();


        }
    }
}