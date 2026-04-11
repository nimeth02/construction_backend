using LevoHubBackend.Application.DTOs.Dashboard;
using LevoHubBackend.Application.Interfaces;
using LevoHubBackend.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LevoHubBackend.Application.Features.Dashboard.Queries.GetUpcomingStageDeadlines;

public class GetUpcomingStageDeadlinesQuery : IRequest<List<UpcomingDeadlineDto>>
{
}

public class GetUpcomingStageDeadlinesQueryHandler : IRequestHandler<GetUpcomingStageDeadlinesQuery, List<UpcomingDeadlineDto>>
{
    private readonly IApplicationDbContext _context;

    public GetUpcomingStageDeadlinesQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<UpcomingDeadlineDto>> Handle(GetUpcomingStageDeadlinesQuery request, CancellationToken cancellationToken)
    {
        return await _context.ProjectStages
            .Include(ps => ps.Stage)
            .Where(ps => ps.IsActive && ps.Status != ProjectStageStatus.Completed && ps.DeadlineDate != null)
            .OrderBy(ps => ps.DeadlineDate)
            .Select(ps => new UpcomingDeadlineDto
            {
                Id = ps.Id,
                Name = ps.Stage != null ? ps.Stage.Name : "Unnamed Stage",
                Status = ps.Status.ToString(),
                DeadlineDate = ps.DeadlineDate
            })
            .ToListAsync(cancellationToken);
    }
}
