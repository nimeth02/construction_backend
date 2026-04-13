using LevoHubBackend.Domain.Common;
using LevoHubBackend.Domain.Enums;
using System;

namespace LevoHubBackend.Domain.Entities;

public class ProjectStage : AuditableEntity<int>
{
    public int ProjectId { get; set; }
    public Project? Project { get; set; }

    public int StageId { get; set; }
    public Stage? Stage { get; set; }

    public DateTime? StartDate { get; set; }
    public DateTime? DeadlineDate { get; set; }
    
    public ProjectStageStatus Status { get; set; } = ProjectStageStatus.Planned;

    public ICollection<ProjectStageTask> ProjectStageTasks { get; set; } = new List<ProjectStageTask>();

    public ICollection<ProjectStageEdge> FromEdges { get; set; } = new List<ProjectStageEdge>();
    public ICollection<ProjectStageEdge> ToEdges { get; set; } = new List<ProjectStageEdge>();
}
