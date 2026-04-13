using MediatR;
using System;

namespace LevoHubBackend.Application.Features.Roles.Commands.CreateRole;

public class CreateRoleCommand : IRequest<Guid>
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
}
