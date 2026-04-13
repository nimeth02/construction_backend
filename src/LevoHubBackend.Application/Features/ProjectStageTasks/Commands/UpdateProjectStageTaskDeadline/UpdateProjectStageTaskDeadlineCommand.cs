using LevoHubBackend.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace LevoHubBackend.Application.Features.ProjectStageTasks.Commands.UpdateProjectStageTaskDeadline;

public class UpdateProjectStageTaskDeadlineCommand : IRequest<bool>
{
    public int Id { get; set; }
    public DateTime? DeadlineDate { get; set; }
}

public class UpdateProjectStageTaskDeadlineCommandHandler : IRequestHandler<UpdateProjectStageTaskDeadlineCommand, bool>
{
    private readonly IApplicationDbContext _context;

    public UpdateProjectStageTaskDeadlineCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(UpdateProjectStageTaskDeadlineCommand request, CancellationToken cancellationToken)
    {
        var task = await _context.ProjectStageTasks
            .FirstOrDefaultAsync(pst => pst.Id == request.Id, cancellationToken);

        if (task == null)
        {
            return false;
        }

        task.DeadlineDate = request.DeadlineDate;
        task.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
