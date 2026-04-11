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

namespace LevoHubBackend.Application.Features.Dashboard.Queries.GetActiveProjects;

public class GetActiveProjectsQuery : IRequest<List<ActiveProjectDto>>
{
}

public class GetActiveProjectsQueryHandler : IRequestHandler<GetActiveProjectsQuery, List<ActiveProjectDto>>
{
    private readonly IApplicationDbContext _context;

    public GetActiveProjectsQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<ActiveProjectDto>> Handle(GetActiveProjectsQuery request, CancellationToken cancellationToken)
    {
        var activeProjects = await _context.Projects
            .Where(p => p.IsActive && p.Status == ProjectStatus.Active)
            .Include(p => p.ProjectStages)
                .ThenInclude(ps => ps.ProjectStageTasks)
            .ToListAsync(cancellationToken);

        return activeProjects.Select(p =>
        {
            var tasks = p.ProjectStages.SelectMany(ps => ps.ProjectStageTasks).ToList();
            var totalTasks = tasks.Count;
            var completedTasks = tasks.Count(t => t.Status == ProjectTaskStatus.Completed);
            
            int progress = totalTasks == 0 ? 0 : (int)((double)completedTasks / totalTasks * 100);

            return new ActiveProjectDto
            {
                Id = p.Id,
                ProjectName = p.Name,
                ClientName = p.ClientName,
                Address = p.Address,
                StartDate = p.StartDate,
                ProgressPercentage = progress,
                TotalTasksCount = totalTasks,
                CompletedTasksCount = completedTasks
            };
        }).ToList();
    }
}
