using LevoHubBackend.Application.DTOs.Templates;
using LevoHubBackend.Application.Features.Templates.Commands.CreateTemplate;
using LevoHubBackend.Application.Features.Templates.Queries.GetTemplateById;
using LevoHubBackend.Application.Features.Templates.Queries.GetTemplates;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LevoHubBackend.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TemplateController : ControllerBase
{
    private readonly IMediator _mediator;

    public TemplateController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult<int>> CreateTemplate(CreateTemplateCommand command)
    {
        var id = await _mediator.Send(command);
        return Ok(id);
    }

    [HttpGet]
    public async Task<ActionResult<List<TemplateDto>>> GetTemplates()
    {
        return await _mediator.Send(new GetTemplatesQuery());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TemplateDto>> GetTemplateById(int id)
    {
        var template = await _mediator.Send(new GetTemplateByIdQuery { Id = id });
        if (template == null)
        {
            return NotFound();
        }
        return Ok(template);
    }
}
