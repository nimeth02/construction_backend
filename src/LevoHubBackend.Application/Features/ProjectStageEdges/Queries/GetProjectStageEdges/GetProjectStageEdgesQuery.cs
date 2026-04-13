using LevoHubBackend.Application.DTOs.ProjectStageEdge;
using LevoHubBackend.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LevoHubBackend.Application.Features.ProjectStageEdges.Queries.GetProjectStageEdges;

public class GetProjectStageEdgesQuery : IRequest<List<ProjectStageEdgeDto>>
{
    public int ProjectId { get; set; }
}

public class GetProjectStageEdgesQueryHandler : IRequestHandler<GetProjectStageEdgesQuery, List<ProjectStageEdgeDto>>
{
    private readonly IApplicationDbContext _context;

    public GetProjectStageEdgesQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<ProjectStageEdgeDto>> Handle(GetProjectStageEdgesQuery request, CancellationToken cancellationToken)
    {
        return await _context.ProjectStageEdges
            .Where(pse => pse.ProjectId == request.ProjectId && pse.IsActive)
            .Include(pse => pse.FromProjectStage)
            .ThenInclude(ps => ps != null ? ps.Stage : null)
            .Include(pse => pse.ToProjectStage)
            .ThenInclude(ps => ps != null ? ps.Stage : null)
            .Select(pse => new ProjectStageEdgeDto
            {
                Id = pse.Id,
                ProjectId = pse.ProjectId,
                FromProjectStageId = pse.FromProjectStageId,
                FromStageName = pse.FromProjectStage != null && pse.FromProjectStage.Stage != null ? pse.FromProjectStage.Stage.Name : string.Empty,
                ToProjectStageId = pse.ToProjectStageId,
                ToStageName = pse.ToProjectStage != null && pse.ToProjectStage.Stage != null ? pse.ToProjectStage.Stage.Name : string.Empty,
                OrderIndex = pse.OrderIndex,
                Condition = pse.Condition,
                LagDays = pse.LagDays,
                EdgeType = pse.EdgeType
            })
            .ToListAsync(cancellationToken);
    }
}
