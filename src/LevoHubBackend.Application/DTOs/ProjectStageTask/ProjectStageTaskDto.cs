using LevoHubBackend.Domain.Enums;
using System;

namespace LevoHubBackend.Application.DTOs.ProjectStageTask;

public class ProjectStageTaskDto
{
    public int Id { get; set; }
    public string TaskName { get; set; } = string.Empty;
    public string TaskDescription { get; set; } = string.Empty;

    public Guid? UserId { get; set; }
    public string? UserName { get; set; }

    public int ProjectStageId { get; set; }
    public string ProjectStageName { get; set; } = string.Empty;

    public DateTime? StartDate { get; set; }
    public DateTime? DeadlineDate { get; set; }

    public ProjectTaskStatus Status { get; set; }
    public string StatusName => Status.ToString();

    public ProjectTaskPriority Priority { get; set; }
    public string PriorityName => Priority.ToString();
}
