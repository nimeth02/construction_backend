using LevoHubBackend.Application.Interfaces;
using LevoHubBackend.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace LevoHubBackend.Application.Features.ProjectStageTasks.Commands.UpdateProjectStageTaskStatus;

public class UpdateProjectStageTaskStatusCommand : IRequest<bool>
{
    public int Id { get; set; }
    public ProjectTaskStatus Status { get; set; }
}

public class UpdateProjectStageTaskStatusCommandHandler : IRequestHandler<UpdateProjectStageTaskStatusCommand, bool>
{
    private readonly IApplicationDbContext _context;

    public UpdateProjectStageTaskStatusCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(UpdateProjectStageTaskStatusCommand request, CancellationToken cancellationToken)
    {
        var task = await _context.ProjectStageTasks
            .FirstOrDefaultAsync(pst => pst.Id == request.Id, cancellationToken);

        if (task == null)
        {
            return false;
        }

        task.Status = request.Status;
        task.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
