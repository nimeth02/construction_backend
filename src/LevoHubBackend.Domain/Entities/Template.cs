using LevoHubBackend.Domain.Common;
using System.Collections.Generic;

namespace LevoHubBackend.Domain.Entities;
public class Template : AuditableEntity<int>
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    public ICollection<TemplateDepartment> TemplateDepartments { get; set; } = new List<TemplateDepartment>();
}