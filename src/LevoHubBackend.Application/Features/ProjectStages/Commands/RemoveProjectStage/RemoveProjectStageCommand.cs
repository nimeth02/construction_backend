using LevoHubBackend.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace LevoHubBackend.Application.Features.ProjectStages.Commands.RemoveProjectStage;

public class RemoveProjectStageCommand : IRequest<bool>
{
    public int Id { get; set; }
}

public class RemoveProjectStageCommandHandler : IRequestHandler<RemoveProjectStageCommand, bool>
{
    private readonly IApplicationDbContext _context;

    public RemoveProjectStageCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(RemoveProjectStageCommand request, CancellationToken cancellationToken)
    {
        var projectStage = await _context.ProjectStages
            .FirstOrDefaultAsync(ps => ps.Id == request.Id, cancellationToken);

        if (projectStage == null)
        {
            return false;
        }

        projectStage.IsActive = false; // Soft delete
        projectStage.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
