using LevoHubBackend.Domain.Common;
using System.Collections.Generic;

namespace LevoHubBackend.Domain.Entities;

public class Department : AuditableEntity<int>
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }

    public ICollection<User> Users { get; set; } = new List<User>();
}
