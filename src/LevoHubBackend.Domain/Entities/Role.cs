using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace LevoHubBackend.Domain.Entities;

public class Role : IdentityRole<Guid>
{
    public string? Description { get; set; }
    public bool IsSystemRole { get; set; }

    public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    
    // Auditable properties
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    public bool IsActive { get; set; } = true;
}
