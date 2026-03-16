using LevoHubBackend.Application.Features.JobTitles.Queries.GetJobTitles;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LevoHubBackend.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class JobTitlesController : ControllerBase
{
    private readonly IMediator _mediator;

    public JobTitlesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<List<JobTitleDto>>> GetJobTitles()
    {
        var jobTitles = await _mediator.Send(new GetJobTitlesQuery());
        return Ok(jobTitles);
    }
}
