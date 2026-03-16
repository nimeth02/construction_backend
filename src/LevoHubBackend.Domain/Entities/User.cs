using LevoHubBackend.Domain.Common;
using System;
using System.Collections.Generic;

namespace LevoHubBackend.Domain.Entities;

public class User : AuditableEntity<Guid>
{
    public string? EmployeeCode { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? PhoneNumber { get; set; }
    public string PasswordHash { get; set; } = string.Empty;
    
    public int? DepartmentId { get; set; }
    public Department? Department { get; set; }

    public int? JobTitleId { get; set; }
    public JobTitle? JobTitle { get; set; }


    public DateTime? DateOfJoining { get; set; }
    public string? ProfilePictureUrl { get; set; }

    public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
}
