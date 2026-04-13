using LevoHubBackend.Application.Interfaces;
using LevoHubBackend.Domain.Entities;
using LevoHubBackend.Domain.Enums;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace LevoHubBackend.Application.Features.ProjectStages.Commands.AddProjectStage;

public class AddProjectStageCommand : IRequest<int>
{
    public int ProjectId { get; set; }
    public int StageId { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? DeadlineDate { get; set; }
    public ProjectStageStatus Status { get; set; } = ProjectStageStatus.Planned;
}

public class AddProjectStageCommandHandler : IRequestHandler<AddProjectStageCommand, int>
{
    private readonly IApplicationDbContext _context;

    public AddProjectStageCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(AddProjectStageCommand request, CancellationToken cancellationToken)
    {
        var projectStage = new ProjectStage
        {
            ProjectId = request.ProjectId,
            StageId = request.StageId,
            StartDate = request.StartDate,
            DeadlineDate = request.DeadlineDate,
            Status = request.Status
        };

        _context.ProjectStages.Add(projectStage);
        await _context.SaveChangesAsync(cancellationToken);

        return projectStage.Id;
    }
}
