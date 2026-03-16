using LevoHubBackend.Domain.Common;
using System.Collections.Generic;

namespace LevoHubBackend.Domain.Entities;

public class Role : AuditableEntity<int>
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public bool IsSystemRole { get; set; }

    public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    public ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();
}
