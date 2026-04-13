using MediatR;
using System.Collections.Generic;

namespace LevoHubBackend.Application.Features.Users.Queries.GetUsersWithRoles;

public class GetUsersWithRolesQuery : IRequest<List<UserWithRolesDto>>
{
}
