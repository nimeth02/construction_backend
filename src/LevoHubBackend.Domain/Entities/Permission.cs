using LevoHubBackend.Domain.Common;
using System;
using System.Collections.Generic;

namespace LevoHubBackend.Domain.Entities;

// Note: Permission does not need CreatedAt/UpdatedAt in all cases but spec asked for CreatedAt.
// I'll inherit BaseEntity and add CreatedAt manually or just use AuditableEntity. 
// Spec said "CreatedAt" only, not UpdatedAt. I'll use simple class or BaseEntity + prop.
public class Permission : BaseEntity<int>
{
    public string Key { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string Module { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();
}
