using System;
using System.Collections.Generic;
using System.Text;

using LevoHubBackend.Domain.Common;

namespace LevoHubBackend.Domain.Entities;

public class Stage : AuditableEntity<int>
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    public int? DepartmentId { get; set; }
    public Department? Department { get; set; }
}

