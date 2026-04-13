using LevoHubBackend.Application.DTOs.ProjectStage;
using LevoHubBackend.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LevoHubBackend.Application.Features.ProjectStages.Queries.GetProjectStages;

public class GetProjectStagesQuery : IRequest<List<ProjectStageDto>>
{
    public int ProjectId { get; set; }
}

public class GetProjectStagesQueryHandler : IRequestHandler<GetProjectStagesQuery, List<ProjectStageDto>>
{
    private readonly IApplicationDbContext _context;

    public GetProjectStagesQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<ProjectStageDto>> Handle(GetProjectStagesQuery request, CancellationToken cancellationToken)
    {
        return await _context.ProjectStages
            .Where(ps => ps.ProjectId == request.ProjectId && ps.IsActive)
            .Include(ps => ps.Project)
            .Include(ps => ps.Stage)
            .Select(ps => new ProjectStageDto
            {
                Id = ps.Id,
                ProjectId = ps.ProjectId,
                ProjectName = ps.Project != null ? ps.Project.Name : string.Empty,
                StageId = ps.StageId,
                StageName = ps.Stage != null ? ps.Stage.Name : string.Empty,
                StartDate = ps.StartDate,
                DeadlineDate = ps.DeadlineDate,
                Status = ps.Status
            })
            .ToListAsync(cancellationToken);
    }
}
