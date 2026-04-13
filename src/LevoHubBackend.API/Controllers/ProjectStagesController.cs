using LevoHubBackend.Application.DTOs.ProjectStage;
using LevoHubBackend.Application.Features.ProjectStages.Commands.AddProjectStage;
using LevoHubBackend.Application.Features.ProjectStages.Commands.RemoveProjectStage;
using LevoHubBackend.Application.Features.ProjectStages.Commands.UpdateProjectStage;
using LevoHubBackend.Application.Features.ProjectStages.Commands.UpdateProjectStageDeadline;
using LevoHubBackend.Application.Features.ProjectStages.Commands.UpdateProjectStageStatus;
using LevoHubBackend.Application.Features.ProjectStages.Queries.GetProjectStages;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LevoHubBackend.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProjectStagesController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProjectStagesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("project/{projectId}")]
    public async Task<ActionResult<List<ProjectStageDto>>> GetProjectStages(int projectId)
    {
        return await _mediator.Send(new GetProjectStagesQuery { ProjectId = projectId });
    }

    [HttpPost]
    public async Task<ActionResult<int>> AddProjectStage(AddProjectStageCommand command)
    {
        var id = await _mediator.Send(command);
        return Ok(id);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProjectStage(int id, UpdateProjectStageCommand command)
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
    public async Task<IActionResult> PatchProjectStageDeadline(int id, UpdateProjectStageDeadlineCommand command)
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
    public async Task<IActionResult> PatchProjectStageStatus(int id, UpdateProjectStageStatusCommand command)
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
    public async Task<IActionResult> RemoveProjectStage(int id)
    {
        var result = await _mediator.Send(new RemoveProjectStageCommand { Id = id });
        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
