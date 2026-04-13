using System;
using System.Collections.Generic;

namespace LevoHubBackend.Application.Features.Users.Queries.GetUsersWithRoles;

public class UserWithRolesDto
{
    public Guid Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public List<string> Roles { get; set; } = new();
}
