using LevoHubBackend.Application.Interfaces;
using LevoHubBackend.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace LevoHubBackend.Application.Features.ProjectStageEdges.Commands.UpdateProjectStageEdge;

public class UpdateProjectStageEdgeCommand : IRequest<bool>
{
    public int Id { get; set; }
    public int FromProjectStageId { get; set; }
    public int ToProjectStageId { get; set; }
    public int OrderIndex { get; set; }
    public string? Condition { get; set; }
    public int LagDays { get; set; }
    public ProjectStageEdgeType EdgeType { get; set; }
}

public class UpdateProjectStageEdgeCommandHandler : IRequestHandler<UpdateProjectStageEdgeCommand, bool>
{
    private readonly IApplicationDbContext _context;

    public UpdateProjectStageEdgeCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(UpdateProjectStageEdgeCommand request, CancellationToken cancellationToken)
    {
        var edge = await _context.ProjectStageEdges
            .FirstOrDefaultAsync(pse => pse.Id == request.Id, cancellationToken);

        if (edge == null)
        {
            return false;
        }

        edge.FromProjectStageId = request.FromProjectStageId;
        edge.ToProjectStageId = request.ToProjectStageId;
        edge.OrderIndex = request.OrderIndex;
        edge.Condition = request.Condition;
        edge.LagDays = request.LagDays;
        edge.EdgeType = request.EdgeType;
        edge.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
