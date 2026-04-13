using LevoHubBackend.Application.Interfaces;
using LevoHubBackend.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LevoHubBackend.Application.Features.Users.Queries.GetUsersWithRoles;

public class GetUsersWithRolesQueryHandler : IRequestHandler<GetUsersWithRolesQuery, List<UserWithRolesDto>>
{
    private readonly UserManager<User> _userManager;

    public GetUsersWithRolesQueryHandler(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public async Task<List<UserWithRolesDto>> Handle(GetUsersWithRolesQuery request, CancellationToken cancellationToken)
    {
        var users = await _userManager.Users
            .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
            .ToListAsync(cancellationToken);

        return users.Select(u => new UserWithRolesDto
        {
            Id = u.Id,
            Email = u.Email ?? string.Empty,
            FirstName = u.FirstName,
            LastName = u.LastName,
            Roles = u.UserRoles.Select(ur => ur.Role.Name ?? string.Empty).ToList()
        }).ToList();
    }
}
