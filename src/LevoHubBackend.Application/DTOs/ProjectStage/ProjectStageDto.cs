using LevoHubBackend.Domain.Enums;
using System;

namespace LevoHubBackend.Application.DTOs.ProjectStage;

public class ProjectStageDto
{
    public int Id { get; set; }
    public int ProjectId { get; set; }
    public string ProjectName { get; set; } = string.Empty;
    public int StageId { get; set; }
    public string StageName { get; set; } = string.Empty;
    public DateTime? StartDate { get; set; }
    public DateTime? DeadlineDate { get; set; }
    public ProjectStageStatus Status { get; set; }
    public string StatusName => Status.ToString();
}
