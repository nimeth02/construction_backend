using LevoHubBackend.Application.Features.Stages.Queries.GetStages;
using MediatR;
using System.Collections.Generic;

namespace LevoHubBackend.Application.Features.Stages.Queries.GetStages;

public class GetStagesQuery : IRequest<List<StageDto>>
{
}
