using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskCollaboration.Api.api.Interfaces;


namespace TaskCollaboration.Api.api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]

public class WorkTaskController : ControllerBase
{
    private readonly IWorkTaskService _workTaskService;

    public WorkTaskController(IWorkTaskService workTaskService)
    {
        _workTaskService = workTaskService;
    }



}