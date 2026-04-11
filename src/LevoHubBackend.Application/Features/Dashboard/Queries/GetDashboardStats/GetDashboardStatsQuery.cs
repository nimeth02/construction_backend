using LevoHubBackend.Application.DTOs.Dashboard;
using LevoHubBackend.Application.Interfaces;
using LevoHubBackend.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LevoHubBackend.Application.Features.Dashboard.Queries.GetDashboardStats;

public class GetDashboardStatsQuery : IRequest<DashboardStatsDto>
{
}

public class GetDashboardStatsQueryHandler : IRequestHandler<GetDashboardStatsQuery, DashboardStatsDto>
{
    private readonly IApplicationDbContext _context;

    public GetDashboardStatsQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<DashboardStatsDto> Handle(GetDashboardStatsQuery request, CancellationToken cancellationToken)
    {
        var now = DateTime.UtcNow;
        var firstDayOfMonth = new DateTime(now.Year, now.Month, 1);
        
        // Use a simple week calculation (last 7 days for simplicity, or actually this week's start)
        int diff = (7 + (now.DayOfWeek - DayOfWeek.Monday)) % 7;
        var firstDayOfWeek = now.AddDays(-1 * diff).Date;

        var totalProjects = await _context.Projects.CountAsync(p => p.IsActive, cancellationToken);
        var totalProjectsThisMonth = await _context.Projects
            .CountAsync(p => p.IsActive && p.CreatedAt >= firstDayOfMonth, cancellationToken);

        var activeProjects = await _context.Projects
            .CountAsync(p => p.IsActive && p.Status == ProjectStatus.Active, cancellationToken);

        var pendingTasks = await _context.ProjectStageTasks
            .CountAsync(t => t.IsActive && (t.Status == ProjectTaskStatus.ToDo || t.Status == ProjectTaskStatus.InProgress), cancellationToken);

        var completedProjects = await _context.Projects
            .CountAsync(p => p.IsActive && p.Status == ProjectStatus.Completed, cancellationToken);
            
        var completedProjectsThisWeek = await _context.Projects
            .CountAsync(p => p.IsActive && p.Status == ProjectStatus.Completed && p.UpdatedAt >= firstDayOfWeek, cancellationToken);

        return new DashboardStatsDto
        {
            TotalProjects = totalProjects,
            TotalProjectsThisMonthChange = totalProjectsThisMonth,
            ActiveProjects = activeProjects,
            PendingTasks = pendingTasks,
            CompletedProjects = completedProjects,
            CompletedProjectsThisWeekChange = completedProjectsThisWeek
        };
    }
}
