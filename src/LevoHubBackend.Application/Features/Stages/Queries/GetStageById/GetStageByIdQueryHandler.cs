using LevoHubBackend.Application.Features.Stages.Queries.GetStages;
using LevoHubBackend.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LevoHubBackend.Application.Features.Stages.Queries.GetStageById;

public class GetStageByIdQueryHandler : IRequestHandler<GetStageByIdQuery, StageDto?>
{
    private readonly IApplicationDbContext _context;

    public GetStageByIdQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<StageDto?> Handle(GetStageByIdQuery request, CancellationToken cancellationToken)
    {
        return await _context.Stages
            .AsNoTracking()
            .Where(s => s.Id == request.Id)
            .Select(s => new StageDto
            {
                Id = s.Id,
                Name = s.Name,
                Description = s.Description
            })
            .FirstOrDefaultAsync(cancellationToken);
    }
}
