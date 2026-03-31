using MediatR;

namespace LevoHubBackend.Application.Features.Stages.Commands.UpdateStage;

public class UpdateStageCommand : IRequest<bool>
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
}
