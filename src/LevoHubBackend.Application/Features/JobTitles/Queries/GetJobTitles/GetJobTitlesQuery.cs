using MediatR;
using System.Collections.Generic;

namespace LevoHubBackend.Application.Features.JobTitles.Queries.GetJobTitles;

public class GetJobTitlesQuery : IRequest<List<JobTitleDto>>
{
}
