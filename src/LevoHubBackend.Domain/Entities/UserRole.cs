using Microsoft.AspNetCore.Identity;
using System;

namespace LevoHubBackend.Domain.Entities;

public class UserRole : IdentityUserRole<Guid>
{
    public User User { get; set; } = null!;
    public Role Role { get; set; } = null!;

    public DateTime AssignedAt { get; set; } = DateTime.UtcNow;
    public Guid? AssignedByUserId { get; set; }
    public User? AssignedByUser { get; set; }
}
