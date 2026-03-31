using LevoHubBackend.Application.Features.Stages.Queries.GetStages;
using MediatR;

namespace LevoHubBackend.Application.Features.Stages.Queries.GetStageById;

public class GetStageByIdQuery : IRequest<StageDto?>
{
    public int Id { get; set; }
}
