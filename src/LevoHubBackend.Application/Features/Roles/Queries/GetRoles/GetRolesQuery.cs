using MediatR;
using System.Collections.Generic;

namespace LevoHubBackend.Application.Features.Roles.Queries.GetRoles;

public class GetRolesQuery : IRequest<List<RoleDto>>
{
}
