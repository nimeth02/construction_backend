using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace LevoHubBackend.Domain.Entities;

public class User : IdentityUser<Guid>
{
    public string? EmployeeCode { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    
    public int? DepartmentId { get; set; }
    public Department? Department { get; set; }

    public int? JobTitleId { get; set; }
    public JobTitle? JobTitle { get; set; }

    public DateTime? DateOfJoining { get; set; }
    public string? ProfilePictureUrl { get; set; }

    public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();

    // Auditable properties (since we cannot inherit AuditableEntity)
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    public bool IsActive { get; set; } = true;
    public ICollection<ProjectStageTask> AssignedTasks { get; set; } = new List<ProjectStageTask>();
}
