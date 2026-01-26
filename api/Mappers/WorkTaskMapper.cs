
using TaskCollaboration.Api.api.Models;
using TaskCollaboration.Api.api.Models.Enums;
using TaskCollaboration.Api.api.DTOs;

namespace TaskCollaboration.Api.api.Mappers;

public static class WorkTaskMapper
{
    //Dto -> Model (veritabanına kaydederken)
    public static WorkTask ToModel(this WorkTaskCreateDto workTaskCreateDto, int userId)
    {
        return new WorkTask
        {

            Title = workTaskCreateDto.Title,
            Description = workTaskCreateDto.Description,
            UserId = userId,
            Status = Enum.Parse<WorkTaskStatus>(workTaskCreateDto.Status),

            Priority = Enum.Parse<WorkTaskPriority>(workTaskCreateDto.Priority)

        };
    }

    //Model -> Dto (veritabanından okurken)
    public static WorkTaskDto ToDto(this WorkTask workTask)
    {

        return new WorkTaskDto
        {

            Id = workTask.Id,
            Title = workTask.Title,
            Description = workTask.Description,

            WorkTaskStatus = workTask.Status.ToString(),
            WorkTaskPriority = workTask.Priority.ToString(),
            DueDate = workTask.DueDate,

        };
    }




}
