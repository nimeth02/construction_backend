using MediatR;
using System;

namespace LevoHubBackend.Application.Features.Roles.Commands.DeleteRole;

public class DeleteRoleCommand : IRequest<bool>
{
    public Guid RoleId { get; set; }
}
