using LevoHubBackend.Application.DTOs.Project;
using LevoHubBackend.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LevoHubBackend.Application.Features.Projects.Queries.GetProjects;

public class GetProjectsQuery : IRequest<List<ProjectDto>>
{
}

public class GetProjectsQueryHandler : IRequestHandler<GetProjectsQuery, List<ProjectDto>>
{
    private readonly IApplicationDbContext _context;

    public GetProjectsQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<ProjectDto>> Handle(GetProjectsQuery request, CancellationToken cancellationToken)
    {
        return await _context.Projects
            .Where(p => p.IsActive)
            .OrderByDescending(p => p.CreatedAt)
            .Select(p => new ProjectDto
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                ClientName = p.ClientName,
                ClientContactNumber = p.ClientContactNumber,
                ClientId = p.ClientId,
                CreatedAt = p.CreatedAt,
                IsActive = p.IsActive
            })
            .ToListAsync(cancellationToken);
    }
}
