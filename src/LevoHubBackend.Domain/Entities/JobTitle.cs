using LevoHubBackend.Domain.Common;
using System.Collections.Generic;

namespace LevoHubBackend.Domain.Entities;

public class JobTitle : AuditableEntity<int>
{
    public string Name { get; set; } = string.Empty;
    public string? Level { get; set; } 
    public string? Description { get; set; }

    public int DepartmentId { get; set; }
    public Department Department { get; set; } = null!;

    public ICollection<User> Users { get; set; } = new List<User>();
}
