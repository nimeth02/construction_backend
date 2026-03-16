using LevoHubBackend.Domain.Enums;
using System;

namespace LevoHubBackend.Domain.Entities;

public class AuditLog : LevoHubBackend.Domain.Common.BaseEntity<Guid>
{

    public string EntityName { get; set; } = string.Empty;
    public string EntityId { get; set; } = string.Empty;
    public string Action { get; set; } = string.Empty; 

    public Guid? PerformedByUserId { get; set; }
    public DateTime PerformedAt { get; set; } = DateTime.UtcNow;
}
