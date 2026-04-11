using LevoHubBackend.Application.DTOs.Dashboard;
using LevoHubBackend.Application.Features.Dashboard.Queries.GetActiveProjects;
using LevoHubBackend.Application.Features.Dashboard.Queries.GetDashboardStats;
using LevoHubBackend.Application.Features.Dashboard.Queries.GetUpcomingProjectDeadlines;
using LevoHubBackend.Application.Features.Dashboard.Queries.GetUpcomingStageDeadlines;
using LevoHubBackend.Application.Features.Dashboard.Queries.GetUpcomingTaskDeadlines;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LevoHubBackend.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DashboardController : ControllerBase
{
    private readonly IMediator _mediator;

    public DashboardController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("stats")]
    public async Task<ActionResult<DashboardStatsDto>> GetDashboardStats()
    {
        return await _mediator.Send(new GetDashboardStatsQuery());
    }

    [HttpGet("active-projects")]
    public async Task<ActionResult<List<ActiveProjectDto>>> GetActiveProjects()
    {
        return await _mediator.Send(new GetActiveProjectsQuery());
    }

    [HttpGet("upcoming-deadlines/projects")]
    public async Task<ActionResult<List<UpcomingDeadlineDto>>> GetUpcomingProjectDeadlines()
    {
        return await _mediator.Send(new GetUpcomingProjectDeadlinesQuery());
    }

    [HttpGet("upcoming-deadlines/stages")]
    public async Task<ActionResult<List<UpcomingDeadlineDto>>> GetUpcomingStageDeadlines()
    {
        return await _mediator.Send(new GetUpcomingStageDeadlinesQuery());
    }

    [HttpGet("upcoming-deadlines/tasks")]
    public async Task<ActionResult<List<UpcomingDeadlineDto>>> GetUpcomingTaskDeadlines()
    {
        return await _mediator.Send(new GetUpcomingTaskDeadlinesQuery());
    }
}
