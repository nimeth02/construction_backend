using LevoHubBackend.Application.Common.Authorization;
using LevoHubBackend.Application.Features.Stages.Commands.CreateStage;
using LevoHubBackend.Application.Features.Stages.Commands.DeleteStage;
using LevoHubBackend.Application.Features.Stages.Commands.UpdateStage;
using LevoHubBackend.Application.Features.Stages.Queries.GetStageById;
using LevoHubBackend.Application.Features.Stages.Queries.GetStages;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LevoHubBackend.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StagesController : ControllerBase
{
    private readonly IMediator _mediator;

    public StagesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Authorize(Policy = Permissions.Stages.View)]
    public async Task<ActionResult<List<StageDto>>> GetStages()
    {
        return await _mediator.Send(new GetStagesQuery());
    }

    [HttpGet("{id}")]
    [Authorize(Policy = Permissions.Stages.View)]
    public async Task<ActionResult<StageDto>> GetStageById(int id)
    {
        var stage = await _mediator.Send(new GetStageByIdQuery { Id = id });
        if (stage == null)
        {
            return NotFound();
        }
        return stage;
    }

    [HttpPost]
    [Authorize(Policy = Permissions.Stages.Create)]
    public async Task<ActionResult<int>> CreateStage(CreateStageCommand command)
    {
        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetStageById), new { id = result }, result);
    }

    [HttpPut("{id}")]
    [Authorize(Policy = Permissions.Stages.Edit)]
    public async Task<IActionResult> UpdateStage(int id, UpdateStageCommand command)
    {
        command.Id = id;
        var result = await _mediator.Send(command);
        if (!result)
        {
            return NotFound();
        }
        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(Policy = Permissions.Stages.Delete)]
    public async Task<IActionResult> DeleteStage(int id)
    {
        var result = await _mediator.Send(new DeleteStageCommand { Id = id });
        if (!result)
        {
            return NotFound();
        }
        return NoContent();
    }
}
