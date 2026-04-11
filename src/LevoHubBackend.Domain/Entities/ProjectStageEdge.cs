using LevoHubBackend.Domain.Common;
using LevoHubBackend.Domain.Enums;
using System;

namespace LevoHubBackend.Domain.Entities;

public class ProjectStageEdge : AuditableEntity<int>
{
    public int ProjectId { get; set; }
    public Project? Project { get; set; }

    public int FromProjectStageId { get; set; }
    public ProjectStage? FromProjectStage { get; set; }

    public int ToProjectStageId { get; set; }
    public ProjectStage? ToProjectStage { get; set; }

    public int OrderIndex { get; set; }
    public string? Condition { get; set; }
    public int LagDays { get; set; } = 0;
    public ProjectStageEdgeType EdgeType { get; set; } = ProjectStageEdgeType.FinishToStart;
}
