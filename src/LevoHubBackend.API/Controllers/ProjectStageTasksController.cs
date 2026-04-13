using LevoHubBackend.Application.DTOs.ProjectStageTask;
using LevoHubBackend.Application.Features.ProjectStageTasks.Commands.CreateProjectStageTask;
using LevoHubBackend.Application.Features.ProjectStageTasks.Commands.DeleteProjectStageTask;
using LevoHubBackend.Application.Features.ProjectStageTasks.Commands.UpdateProjectStageTask;
using LevoHubBackend.Application.Features.ProjectStageTasks.Commands.UpdateProjectStageTaskDeadline;
using LevoHubBackend.Application.Features.ProjectStageTasks.Commands.UpdateProjectStageTaskStatus;
using LevoHubBackend.Application.Features.ProjectStageTasks.Queries.GetProjectStageTasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LevoHubBackend.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProjectStageTasksController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProjectStageTasksController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("stage/{projectStageId}")]
    public async Task<ActionResult<List<ProjectStageTaskDto>>> GetProjectStageTasks(int projectStageId)
    {
        return await _mediator.Send(new GetProjectStageTasksQuery { ProjectStageId = projectStageId });
    }

    [HttpPost]
    public async Task<ActionResult<int>> CreateProjectStageTask(CreateProjectStageTaskCommand command)
    {
        var id = await _mediator.Send(command);
        return Ok(id);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProjectStageTask(int id, UpdateProjectStageTaskCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest();
        }

        var result = await _mediator.Send(command);
        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpPatch("{id}/deadline")]
    public async Task<IActionResult> PatchProjectStageTaskDeadline(int id, UpdateProjectStageTaskDeadlineCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest();
        }

        var result = await _mediator.Send(command);
        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpPatch("{id}/status")]
    public async Task<IActionResult> PatchProjectStageTaskStatus(int id, UpdateProjectStageTaskStatusCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest();
        }

        var result = await _mediator.Send(command);
        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProjectStageTask(int id)
    {
        var result = await _mediator.Send(new DeleteProjectStageTaskCommand { Id = id });
        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
