using LevoHubBackend.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace LevoHubBackend.Application.Features.ProjectStageTasks.Commands.DeleteProjectStageTask;

public class DeleteProjectStageTaskCommand : IRequest<bool>
{
    public int Id { get; set; }
}

public class DeleteProjectStageTaskCommandHandler : IRequestHandler<DeleteProjectStageTaskCommand, bool>
{
    private readonly IApplicationDbContext _context;

    public DeleteProjectStageTaskCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(DeleteProjectStageTaskCommand request, CancellationToken cancellationToken)
    {
        var task = await _context.ProjectStageTasks
            .FirstOrDefaultAsync(pst => pst.Id == request.Id, cancellationToken);

        if (task == null)
        {
            return false;
        }

        task.IsActive = false; // Soft delete
        task.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
