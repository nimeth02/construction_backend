using LevoHubBackend.Application.Interfaces;
using LevoHubBackend.Domain.Entities;
using LevoHubBackend.Domain.Enums;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace LevoHubBackend.Application.Features.Projects.Commands.CreateProject;

public class CreateProjectCommand : IRequest<int>
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string ClientName { get; set; } = string.Empty;
    public string ClientContactNumber { get; set; } = string.Empty;
    public string ClientId { get; set; } = string.Empty;
    public ProjectStatus Status { get; set; } = ProjectStatus.Active;
}

public class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateProjectCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
    {
        var project = new Project
        {
            Name = request.Name,
            Description = request.Description,
            ClientName = request.ClientName,
            ClientContactNumber = request.ClientContactNumber,
            ClientId = request.ClientId,
            Status = request.Status
        };

        _context.Projects.Add(project);
        await _context.SaveChangesAsync(cancellationToken);

        return project.Id;
    }
}
