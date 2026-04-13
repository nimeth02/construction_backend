using LevoHubBackend.Application.DTOs.Project;
using LevoHubBackend.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace LevoHubBackend.Application.Features.Projects.Queries.GetProjectById;

public class GetProjectByIdQuery : IRequest<ProjectDto?>
{
    public int Id { get; set; }
}

public class GetProjectByIdQueryHandler : IRequestHandler<GetProjectByIdQuery, ProjectDto?>
{
    private readonly IApplicationDbContext _context;

    public GetProjectByIdQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ProjectDto?> Handle(GetProjectByIdQuery request, CancellationToken cancellationToken)
    {
        var project = await _context.Projects
            .FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

        if (project == null)
        {
            return null;
        }

        return new ProjectDto
        {
            Id = project.Id,
            Name = project.Name,
            Description = project.Description,
            ClientName = project.ClientName,
            ClientContactNumber = project.ClientContactNumber,
            ClientId = project.ClientId,
            CreatedAt = project.CreatedAt,
            IsActive = project.IsActive
        };
    }
}
