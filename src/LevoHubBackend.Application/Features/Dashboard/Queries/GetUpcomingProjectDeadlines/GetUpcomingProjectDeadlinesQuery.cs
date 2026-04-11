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

namespace LevoHubBackend.Application.Features.Dashboard.Queries.GetUpcomingProjectDeadlines;

public class GetUpcomingProjectDeadlinesQuery : IRequest<List<UpcomingDeadlineDto>>
{
}

public class GetUpcomingProjectDeadlinesQueryHandler : IRequestHandler<GetUpcomingProjectDeadlinesQuery, List<UpcomingDeadlineDto>>
{
    private readonly IApplicationDbContext _context;

    public GetUpcomingProjectDeadlinesQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<UpcomingDeadlineDto>> Handle(GetUpcomingProjectDeadlinesQuery request, CancellationToken cancellationToken)
    {
        return await _context.Projects
            .Where(p => p.IsActive && p.Status != ProjectStatus.Completed && p.DeadlineDate != null)
            .OrderBy(p => p.DeadlineDate)
            .Select(p => new UpcomingDeadlineDto
            {
                Id = p.Id,
                Name = p.Name,
                Status = p.Status.ToString(),
                DeadlineDate = p.DeadlineDate
            })
            .ToListAsync(cancellationToken);
    }
}
