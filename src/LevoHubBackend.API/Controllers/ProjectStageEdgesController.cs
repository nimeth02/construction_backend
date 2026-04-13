using LevoHubBackend.Application.DTOs.ProjectStageEdge;
using LevoHubBackend.Application.Features.ProjectStageEdges.Commands.CreateProjectStageEdge;
using LevoHubBackend.Application.Features.ProjectStageEdges.Commands.DeleteProjectStageEdge;
using LevoHubBackend.Application.Features.ProjectStageEdges.Commands.UpdateProjectStageEdge;
using LevoHubBackend.Application.Features.ProjectStageEdges.Queries.GetProjectStageEdges;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LevoHubBackend.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProjectStageEdgesController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProjectStageEdgesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("project/{projectId}")]
    public async Task<ActionResult<List<ProjectStageEdgeDto>>> GetProjectStageEdges(int projectId)
    {
        return await _mediator.Send(new GetProjectStageEdgesQuery { ProjectId = projectId });
    }

    [HttpPost]
    public async Task<ActionResult<int>> CreateProjectStageEdge(CreateProjectStageEdgeCommand command)
    {
        var id = await _mediator.Send(command);
        return Ok(id);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProjectStageEdge(int id, UpdateProjectStageEdgeCommand command)
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
    public async Task<IActionResult> DeleteProjectStageEdge(int id)
    {
        var result = await _mediator.Send(new DeleteProjectStageEdgeCommand { Id = id });
        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
