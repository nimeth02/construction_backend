using System;

namespace LevoHubBackend.Application.DTOs.Dashboard;

public class ActiveProjectDto
{
    public int Id { get; set; }
    public string ProjectName { get; set; } = string.Empty;
    public string ClientName { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public int ProgressPercentage { get; set; }
    public int TotalTasksCount { get; set; }
    public int CompletedTasksCount { get; set; }
    public DateTime? StartDate { get; set; }
    public string FormattedStartDate => StartDate?.ToString("MMM d, yyyy") ?? "Not Started";
    public string TaskStatusSummary => $"{CompletedTasksCount} of {TotalTasksCount} tasks completed";
}
