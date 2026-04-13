using MediatR;
using System;
using System.Collections.Generic;

namespace LevoHubBackend.Application.Features.Users.Commands.UpdateUserRoles;

public class UpdateUserRolesCommand : IRequest<bool>
{
    public Guid UserId { get; set; }
    public List<string> Roles { get; set; } = new();
}
