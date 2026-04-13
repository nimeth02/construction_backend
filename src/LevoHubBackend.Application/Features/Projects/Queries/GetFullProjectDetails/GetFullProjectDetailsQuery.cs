using LevoHubBackend.Application.DTOs.Project;
using LevoHubBackend.Application.DTOs.ProjectStageEdge;
using LevoHubBackend.Application.DTOs.ProjectStageTask;
using LevoHubBackend.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LevoHubBackend.Application.Features.Projects.Queries.GetFullProjectDetails;

public class GetFullProjectDetailsQuery : IRequest<FullProjectDetailsDto?>
{
    public int Id { get; set; }
}

public class GetFullProjectDetailsQueryHandler : IRequestHandler<GetFullProjectDetailsQuery, FullProjectDetailsDto?>
{
    private readonly IApplicationDbContext _context;

    public GetFullProjectDetailsQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<FullProjectDetailsDto?> Handle(GetFullProjectDetailsQuery request, CancellationToken cancellationToken)
    {
        var project = await _context.Projects
            .Include(p => p.ProjectStages)
                .ThenInclude(ps => ps.Stage)
            .Include(p => p.ProjectStages)
                .ThenInclude(ps => ps.ProjectStageTasks)
                    .ThenInclude(pst => pst.AssignedUser)
            .Include(p => p.ProjectStageEdges)
                .ThenInclude(pse => pse.FromProjectStage)
                    .ThenInclude(ps => ps.Stage)
            .Include(p => p.ProjectStageEdges)
                .ThenInclude(pse => pse.ToProjectStage)
                    .ThenInclude(ps => ps.Stage)
            .FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

        if (project == null) return null;

        return new FullProjectDetailsDto
        {
            Project = new ProjectDto
            {
                Id = project.Id,
                Name = project.Name,
                Description = project.Description,
                ClientName = project.ClientName,
                ClientContactNumber = project.ClientContactNumber,
                ClientId = project.ClientId,
                CreatedAt = project.CreatedAt,
                IsActive = project.IsActive,
                Address = project.Address,
                StartDate = project.StartDate,
                DeadlineDate = project.DeadlineDate,
                Status = project.Status
            },
            Stages = project.ProjectStages.Select(ps => new ProjectStageWithTasksDto
            {
                Id = ps.Id,
                ProjectId = ps.ProjectId,
                ProjectName = project.Name,
                StageId = ps.StageId,
                StageName = ps.Stage?.Name ?? string.Empty,
                StartDate = ps.StartDate,
                DeadlineDate = ps.DeadlineDate,
                Status = ps.Status,
                Tasks = ps.ProjectStageTasks.Select(pst => new ProjectStageTaskDto
                {
                    Id = pst.Id,
                    TaskName = pst.TaskName,
                    TaskDescription = pst.TaskDescription,
                    UserId = pst.UserId,
                    UserName = pst.AssignedUser != null ? pst.AssignedUser.FirstName + " " + pst.AssignedUser.LastName : null,
                    ProjectStageId = pst.ProjectStageId,
                    ProjectStageName = ps.Stage?.Name ?? string.Empty,
                    StartDate = pst.StartDate,
                    DeadlineDate = pst.DeadlineDate,
                    Status = pst.Status,
                    Priority = pst.Priority
                }).ToList()
            }).ToList(),
            WorkflowEdges = project.ProjectStageEdges.Select(pse => new ProjectStageEdgeDto
            {
                Id = pse.Id,
                ProjectId = pse.ProjectId,
                FromProjectStageId = pse.FromProjectStageId,
                FromStageName = pse.FromProjectStage?.Stage?.Name ?? string.Empty,
                ToProjectStageId = pse.ToProjectStageId,
                ToStageName = pse.ToProjectStage?.Stage?.Name ?? string.Empty,
                OrderIndex = pse.OrderIndex,
                Condition = pse.Condition,
                LagDays = pse.LagDays,
                EdgeType = pse.EdgeType
            }).ToList()
        };
    }
}
