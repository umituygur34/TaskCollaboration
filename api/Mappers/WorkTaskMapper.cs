using TaskCollaboration.Api.DTOs.WorkTaskDto;
using TaskCollaboration.Api.Models;


namespace TaskCollaboration.Api.Mappers;

public static class WorkTaskMapper
{
    //Dto -> Model (veritabanına kaydederken)
    public static WorkTask ToModel(this CreateWorkTaskDto createWorkTaskDto)
    {
        return new WorkTask
        {

            Title = createWorkTaskDto.Title,
            Description = createWorkTaskDto.Description,

            Status = createWorkTaskDto.Status,
            Priority = createWorkTaskDto.Priority,
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

    //update
    public static WorkTask UpdateToModel(this WorkTask workTask, UpdateWorkTaskDto updateWorkTaskDto)
    {
        workTask.Title = updateWorkTaskDto.Title;
        workTask.Description = updateWorkTaskDto.Description;
        workTask.Status = updateWorkTaskDto.Status;
        workTask.Priority = updateWorkTaskDto.Priority;
        workTask.DueDate = updateWorkTaskDto.DueDate;

        return workTask;

    }




}
