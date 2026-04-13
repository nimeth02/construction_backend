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

namespace LevoHubBackend.Application.Features.Dashboard.Queries.GetUpcomingTaskDeadlines;

public class GetUpcomingTaskDeadlinesQuery : IRequest<List<UpcomingDeadlineDto>>
{
}

public class GetUpcomingTaskDeadlinesQueryHandler : IRequestHandler<GetUpcomingTaskDeadlinesQuery, List<UpcomingDeadlineDto>>
{
    private readonly IApplicationDbContext _context;

    public GetUpcomingTaskDeadlinesQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<UpcomingDeadlineDto>> Handle(GetUpcomingTaskDeadlinesQuery request, CancellationToken cancellationToken)
    {
        return await _context.ProjectStageTasks
            .Where(pst => pst.IsActive && pst.Status != ProjectTaskStatus.Completed && pst.DeadlineDate != null)
            .OrderBy(pst => pst.DeadlineDate)
            .Select(pst => new UpcomingDeadlineDto
            {
                Id = pst.Id,
                Name = pst.TaskName,
                Status = pst.Status.ToString(),
                DeadlineDate = pst.DeadlineDate
            })
            .ToListAsync(cancellationToken);
    }
}
