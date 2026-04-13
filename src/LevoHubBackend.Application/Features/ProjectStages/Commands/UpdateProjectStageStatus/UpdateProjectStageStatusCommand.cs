using LevoHubBackend.Application.Interfaces;
using LevoHubBackend.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace LevoHubBackend.Application.Features.ProjectStages.Commands.UpdateProjectStageStatus;

public class UpdateProjectStageStatusCommand : IRequest<bool>
{
    public int Id { get; set; }
    public ProjectStageStatus Status { get; set; }
}

public class UpdateProjectStageStatusCommandHandler : IRequestHandler<UpdateProjectStageStatusCommand, bool>
{
    private readonly IApplicationDbContext _context;

    public UpdateProjectStageStatusCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(UpdateProjectStageStatusCommand request, CancellationToken cancellationToken)
    {
        var projectStage = await _context.ProjectStages
            .FirstOrDefaultAsync(ps => ps.Id == request.Id, cancellationToken);

        if (projectStage == null)
        {
            return false;
        }

        projectStage.Status = request.Status;
        projectStage.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
