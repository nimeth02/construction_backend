using MediatR;
using System;

namespace LevoHubBackend.Application.Features.Roles.Commands.RevokePermission;

public class RevokePermissionCommand : IRequest<bool>
{
    public Guid RoleId { get; set; }
    public string Permission { get; set; } = string.Empty;
}
