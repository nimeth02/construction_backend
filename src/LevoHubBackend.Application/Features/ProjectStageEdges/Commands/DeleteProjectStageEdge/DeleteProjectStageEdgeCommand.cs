using LevoHubBackend.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace LevoHubBackend.Application.Features.ProjectStageEdges.Commands.DeleteProjectStageEdge;

public class DeleteProjectStageEdgeCommand : IRequest<bool>
{
    public int Id { get; set; }
}

public class DeleteProjectStageEdgeCommandHandler : IRequestHandler<DeleteProjectStageEdgeCommand, bool>
{
    private readonly IApplicationDbContext _context;

    public DeleteProjectStageEdgeCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(DeleteProjectStageEdgeCommand request, CancellationToken cancellationToken)
    {
        var edge = await _context.ProjectStageEdges
            .FirstOrDefaultAsync(pse => pse.Id == request.Id, cancellationToken);

        if (edge == null)
        {
            return false;
        }

        edge.IsActive = false; // Soft delete
        edge.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
