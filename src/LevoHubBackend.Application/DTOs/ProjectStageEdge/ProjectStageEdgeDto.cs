using LevoHubBackend.Domain.Enums;

namespace LevoHubBackend.Application.DTOs.ProjectStageEdge;

public class ProjectStageEdgeDto
{
    public int Id { get; set; }
    public int ProjectId { get; set; }
    
    public int FromProjectStageId { get; set; }
    public string FromStageName { get; set; } = string.Empty;

    public int ToProjectStageId { get; set; }
    public string ToStageName { get; set; } = string.Empty;

    public int OrderIndex { get; set; }
    public string? Condition { get; set; }
    public int LagDays { get; set; }
    public ProjectStageEdgeType EdgeType { get; set; }
    public string EdgeTypeName => EdgeType.ToString();
}
