using LevoHubBackend.Application.DTOs.ProjectStage;
using LevoHubBackend.Application.DTOs.ProjectStageTask;
using System.Collections.Generic;

namespace LevoHubBackend.Application.DTOs.Project;

public class ProjectStageWithTasksDto : ProjectStageDto
{
    public List<ProjectStageTaskDto> Tasks { get; set; } = new List<ProjectStageTaskDto>();
}
