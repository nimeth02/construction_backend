using LevoHubBackend.Application.Interfaces;
using LevoHubBackend.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace LevoHubBackend.Application.Features.Projects.Commands.UpdateProject;

public class UpdateProjectCommand : IRequest<bool>
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string ClientName { get; set; } = string.Empty;
    public string ClientContactNumber { get; set; } = string.Empty;
    public string ClientId { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public ProjectStatus Status { get; set; }
}

public class UpdateProjectCommandHandler : IRequestHandler<UpdateProjectCommand, bool>
{
    private readonly IApplicationDbContext _context;

    public UpdateProjectCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
    {
        var project = await _context.Projects
            .FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

        if (project == null)
        {
            return false;
        }

        project.Name = request.Name;
        project.Description = request.Description;
        project.ClientName = request.ClientName;
        project.ClientContactNumber = request.ClientContactNumber;
        project.ClientId = request.ClientId;
        project.IsActive = request.IsActive;
        project.Status = request.Status;
        project.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
