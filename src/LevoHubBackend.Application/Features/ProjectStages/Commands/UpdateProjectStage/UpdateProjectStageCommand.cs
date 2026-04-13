using LevoHubBackend.Application.Interfaces;
using LevoHubBackend.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace LevoHubBackend.Application.Features.ProjectStages.Commands.UpdateProjectStage;

public class UpdateProjectStageCommand : IRequest<bool>
{
    public int Id { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? DeadlineDate { get; set; }
    public ProjectStageStatus Status { get; set; }
}

public class UpdateProjectStageCommandHandler : IRequestHandler<UpdateProjectStageCommand, bool>
{
    private readonly IApplicationDbContext _context;

    public UpdateProjectStageCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(UpdateProjectStageCommand request, CancellationToken cancellationToken)
    {
        var projectStage = await _context.ProjectStages
            .FirstOrDefaultAsync(ps => ps.Id == request.Id, cancellationToken);

        if (projectStage == null)
        {
            return false;
        }

        projectStage.StartDate = request.StartDate;
        projectStage.DeadlineDate = request.DeadlineDate;
        projectStage.Status = request.Status;
        projectStage.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
