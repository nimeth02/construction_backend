using MediatR;

namespace LevoHubBackend.Application.Features.Stages.Commands.DeleteStage;

public class DeleteStageCommand : IRequest<bool>
{
    public int Id { get; set; }
}
