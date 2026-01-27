
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
            Status = workTaskCreateDto.Status,
            Priority = workTaskCreateDto.Priority,

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

            Status = workTask.Status,
            Priority = workTask.Priority,
            DueDate = workTask.DueDate,

        };
    }




}
