using LevoHubBackend.Domain.Common;
using LevoHubBackend.Domain.Enums;
using System;

namespace LevoHubBackend.Domain.Entities;

public class ProjectStageTask : AuditableEntity<int>
{
    public string TaskName { get; set; } = string.Empty;
    public string TaskDescription { get; set; } = string.Empty;

    public Guid? UserId { get; set; }
    public User? AssignedUser { get; set; }

    public int ProjectStageId { get; set; }
    public ProjectStage? ProjectStage { get; set; }

    public DateTime? StartDate { get; set; }
    public DateTime? DeadlineDate { get; set; }

    public ProjectTaskStatus Status { get; set; } = ProjectTaskStatus.ToDo;
    public ProjectTaskPriority Priority { get; set; } = ProjectTaskPriority.Medium;
}
