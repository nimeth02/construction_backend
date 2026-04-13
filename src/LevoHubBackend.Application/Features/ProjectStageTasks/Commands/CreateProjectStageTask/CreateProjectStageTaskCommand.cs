using LevoHubBackend.Application.Interfaces;
using LevoHubBackend.Domain.Entities;
using LevoHubBackend.Domain.Enums;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace LevoHubBackend.Application.Features.ProjectStageTasks.Commands.CreateProjectStageTask;

public class CreateProjectStageTaskCommand : IRequest<int>
{
    public string TaskName { get; set; } = string.Empty;
    public string TaskDescription { get; set; } = string.Empty;
    public Guid? UserId { get; set; }
    public int ProjectStageId { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? DeadlineDate { get; set; }
    public ProjectTaskStatus Status { get; set; } = ProjectTaskStatus.ToDo;
    public ProjectTaskPriority Priority { get; set; } = ProjectTaskPriority.Medium;
}

public class CreateProjectStageTaskCommandHandler : IRequestHandler<CreateProjectStageTaskCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateProjectStageTaskCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateProjectStageTaskCommand request, CancellationToken cancellationToken)
    {
        var task = new ProjectStageTask
        {
            TaskName = request.TaskName,
            TaskDescription = request.TaskDescription,
            UserId = request.UserId,
            ProjectStageId = request.ProjectStageId,
            StartDate = request.StartDate,
            DeadlineDate = request.DeadlineDate,
            Status = request.Status,
            Priority = request.Priority
        };

        _context.ProjectStageTasks.Add(task);
        await _context.SaveChangesAsync(cancellationToken);

        return task.Id;
    }
}
