using LevoHubBackend.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LevoHubBackend.Application.Features.JobTitles.Queries.GetJobTitles;

public class GetJobTitlesQueryHandler : IRequestHandler<GetJobTitlesQuery, List<JobTitleDto>>
{
    private readonly IApplicationDbContext _context;

    public GetJobTitlesQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<JobTitleDto>> Handle(GetJobTitlesQuery request, CancellationToken cancellationToken)
    {
        var jobTitles = await _context.JobTitles
            .AsNoTracking()
            .Include(j => j.Department)
            .Select(j => new JobTitleDto
            {
                Id = j.Id,
                Name = j.Name,
                Level = j.Level,
                Description = j.Description,
                DepartmentId = j.DepartmentId,
                DepartmentName = j.Department.Name 
            })
            .ToListAsync(cancellationToken);

        return jobTitles;
    }
}
