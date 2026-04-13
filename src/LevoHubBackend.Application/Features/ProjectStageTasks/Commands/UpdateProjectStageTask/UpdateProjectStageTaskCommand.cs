using LevoHubBackend.Application.Interfaces;
using LevoHubBackend.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace LevoHubBackend.Application.Features.ProjectStageTasks.Commands.UpdateProjectStageTask;

public class UpdateProjectStageTaskCommand : IRequest<bool>
{
    public int Id { get; set; }
    public string TaskName { get; set; } = string.Empty;
    public string TaskDescription { get; set; } = string.Empty;
    public Guid? UserId { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? DeadlineDate { get; set; }
    public ProjectTaskStatus Status { get; set; }
    public ProjectTaskPriority Priority { get; set; }
}

public class UpdateProjectStageTaskCommandHandler : IRequestHandler<UpdateProjectStageTaskCommand, bool>
{
    private readonly IApplicationDbContext _context;

    public UpdateProjectStageTaskCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(UpdateProjectStageTaskCommand request, CancellationToken cancellationToken)
    {
        var task = await _context.ProjectStageTasks
            .FirstOrDefaultAsync(pst => pst.Id == request.Id, cancellationToken);

        if (task == null)
        {
            return false;
        }

        task.TaskName = request.TaskName;
        task.TaskDescription = request.TaskDescription;
        task.UserId = request.UserId;
        task.StartDate = request.StartDate;
        task.DeadlineDate = request.DeadlineDate;
        task.Status = request.Status;
        task.Priority = request.Priority;
        task.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
