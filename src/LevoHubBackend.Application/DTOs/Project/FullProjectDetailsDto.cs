using LevoHubBackend.Application.DTOs.ProjectStageEdge;
using System.Collections.Generic;

namespace LevoHubBackend.Application.DTOs.Project;

public class FullProjectDetailsDto
{
    public ProjectDto Project { get; set; } = null!;
    public List<ProjectStageWithTasksDto> Stages { get; set; } = new List<ProjectStageWithTasksDto>();
    public List<ProjectStageEdgeDto> WorkflowEdges { get; set; } = new List<ProjectStageEdgeDto>();
}
