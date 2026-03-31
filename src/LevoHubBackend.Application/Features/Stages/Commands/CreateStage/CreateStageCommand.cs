using MediatR;

namespace LevoHubBackend.Application.Features.Stages.Commands.CreateStage;

public class CreateStageCommand : IRequest<int>
{
    public string Name { get; set; } = string.Empty; 
    public string Description { get; set; } = string.Empty;
    public int? DepartmentId { get; set; }
}
