using LevoHubBackend.Application.DTOs.Project;
using LevoHubBackend.Application.Features.Projects.Commands.CreateProject;
using LevoHubBackend.Application.Features.Projects.Commands.DeleteProject;
using LevoHubBackend.Application.Features.Projects.Commands.UpdateProject;
using LevoHubBackend.Application.Features.Projects.Commands.UpdateProjectStatus;
using LevoHubBackend.Application.Features.Projects.Queries.GetProjectById;
using LevoHubBackend.Application.Features.Projects.Queries.GetProjects;
using LevoHubBackend.Application.Features.Projects.Queries.GetFullProjectDetails;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LevoHubBackend.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProjectsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProjectsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<List<ProjectDto>>> GetProjects()
    {
        return await _mediator.Send(new GetProjectsQuery());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProjectDto>> GetProjectById(int id)
    {
        var project = await _mediator.Send(new GetProjectByIdQuery { Id = id });
        if (project == null)
        {
            return NotFound();
        }
        return project;
    }

    [HttpGet("{id}/full")]
    public async Task<ActionResult<FullProjectDetailsDto>> GetFullProject(int id)
    {
        var project = await _mediator.Send(new GetFullProjectDetailsQuery { Id = id });
        if (project == null)
        {
            return NotFound();
        }
        return Ok(project);
    }

    [HttpPost]
    public async Task<ActionResult<int>> CreateProject(CreateProjectCommand command)
    {
        var id = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetProjectById), new { id }, id);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProject(int id, UpdateProjectCommand command)
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
    public async Task<IActionResult> PatchProjectStatus(int id, UpdateProjectStatusCommand command)
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
    public async Task<IActionResult> DeleteProject(int id)
    {
        var result = await _mediator.Send(new DeleteProjectCommand { Id = id });
        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
