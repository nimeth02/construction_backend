using MediatR;
using System;

namespace LevoHubBackend.Application.Features.Roles.Commands.AssignPermission;

public class AssignPermissionCommand : IRequest<bool>
{
    public Guid RoleId { get; set; }
    public string Permission { get; set; } = string.Empty;
}
