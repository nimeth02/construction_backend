using LevoHubBackend.Application.Features.Stages.Queries.GetStages;
using LevoHubBackend.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LevoHubBackend.Application.Features.Stages.Queries.GetStages;

public class GetStagesQueryHandler : IRequestHandler<GetStagesQuery, List<StageDto>>
{
    private readonly IApplicationDbContext _context;

    public GetStagesQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<StageDto>> Handle(GetStagesQuery request, CancellationToken cancellationToken)
    {
        return await _context.Stages
            .AsNoTracking()
            .Select(s => new StageDto
            {
                Id = s.Id,
                Name = s.Name,
                Description = s.Description
            })
            .ToListAsync(cancellationToken);
    }
}
