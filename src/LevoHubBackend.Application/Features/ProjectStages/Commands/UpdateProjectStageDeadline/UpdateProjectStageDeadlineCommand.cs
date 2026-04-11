using LevoHubBackend.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace LevoHubBackend.Application.Features.ProjectStages.Commands.UpdateProjectStageDeadline;

public class UpdateProjectStageDeadlineCommand : IRequest<bool>
{
    public int Id { get; set; }
    public DateTime? DeadlineDate { get; set; }
}

public class UpdateProjectStageDeadlineCommandHandler : IRequestHandler<UpdateProjectStageDeadlineCommand, bool>
{
    private readonly IApplicationDbContext _context;

    public UpdateProjectStageDeadlineCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(UpdateProjectStageDeadlineCommand request, CancellationToken cancellationToken)
    {
        var projectStage = await _context.ProjectStages
            .FirstOrDefaultAsync(ps => ps.Id == request.Id, cancellationToken);

        if (projectStage == null)
        {
            return false;
        }

        projectStage.DeadlineDate = request.DeadlineDate;
        projectStage.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
