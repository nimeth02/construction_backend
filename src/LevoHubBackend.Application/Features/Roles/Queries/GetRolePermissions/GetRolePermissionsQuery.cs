using MediatR;
using System;
using System.Collections.Generic;

namespace LevoHubBackend.Application.Features.Roles.Queries.GetRolePermissions;

public class GetRolePermissionsQuery : IRequest<List<string>>
{
    public Guid RoleId { get; set; }
}
