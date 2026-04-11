using LevoHubBackend.Application.DTOs.ProjectStageTask;
using LevoHubBackend.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LevoHubBackend.Application.Features.ProjectStageTasks.Queries.GetProjectStageTasks;

public class GetProjectStageTasksQuery : IRequest<List<ProjectStageTaskDto>>
{
    public int ProjectStageId { get; set; }
}

public class GetProjectStageTasksQueryHandler : IRequestHandler<GetProjectStageTasksQuery, List<ProjectStageTaskDto>>
{
    private readonly IApplicationDbContext _context;

    public GetProjectStageTasksQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<ProjectStageTaskDto>> Handle(GetProjectStageTasksQuery request, CancellationToken cancellationToken)
    {
        return await _context.ProjectStageTasks
            .Where(pst => pst.ProjectStageId == request.ProjectStageId && pst.IsActive)
            .Include(pst => pst.AssignedUser)
            .Include(pst => pst.ProjectStage)
            .ThenInclude(ps => ps != null ? ps.Stage : null)
            .Select(pst => new ProjectStageTaskDto
            {
                Id = pst.Id,
                TaskName = pst.TaskName,
                TaskDescription = pst.TaskDescription,
                UserId = pst.UserId,
                UserName = pst.AssignedUser != null ? pst.AssignedUser.FirstName + " " + pst.AssignedUser.LastName : null,
                ProjectStageId = pst.ProjectStageId,
                ProjectStageName = pst.ProjectStage != null && pst.ProjectStage.Stage != null ? pst.ProjectStage.Stage.Name : string.Empty,
                StartDate = pst.StartDate,
                DeadlineDate = pst.DeadlineDate,
                Status = pst.Status,
                Priority = pst.Priority
            })
            .ToListAsync(cancellationToken);
    }
}
