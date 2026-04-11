using LevoHubBackend.Application.Interfaces;
using LevoHubBackend.Domain.Entities;
using LevoHubBackend.Domain.Enums;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace LevoHubBackend.Application.Features.ProjectStageEdges.Commands.CreateProjectStageEdge;

public class CreateProjectStageEdgeCommand : IRequest<int>
{
    public int ProjectId { get; set; }
    public int FromProjectStageId { get; set; }
    public int ToProjectStageId { get; set; }
    public int OrderIndex { get; set; }
    public string? Condition { get; set; }
    public int LagDays { get; set; }
    public ProjectStageEdgeType EdgeType { get; set; } = ProjectStageEdgeType.FinishToStart;
}

public class CreateProjectStageEdgeCommandHandler : IRequestHandler<CreateProjectStageEdgeCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateProjectStageEdgeCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateProjectStageEdgeCommand request, CancellationToken cancellationToken)
    {
        var edge = new ProjectStageEdge
        {
            ProjectId = request.ProjectId,
            FromProjectStageId = request.FromProjectStageId,
            ToProjectStageId = request.ToProjectStageId,
            OrderIndex = request.OrderIndex,
            Condition = request.Condition,
            LagDays = request.LagDays,
            EdgeType = request.EdgeType
        };

        _context.ProjectStageEdges.Add(edge);
        await _context.SaveChangesAsync(cancellationToken);

        return edge.Id;
    }
}
