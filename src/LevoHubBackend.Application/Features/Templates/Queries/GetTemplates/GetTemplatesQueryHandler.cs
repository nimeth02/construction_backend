using LevoHubBackend.Application.DTOs.Department;
using LevoHubBackend.Application.DTOs.Templates;
using LevoHubBackend.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LevoHubBackend.Application.Features.Templates.Queries.GetTemplates;

public class GetTemplatesQueryHandler : IRequestHandler<GetTemplatesQuery, List<TemplateDto>>
{
    private readonly IApplicationDbContext _context;

    public GetTemplatesQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<TemplateDto>> Handle(GetTemplatesQuery request, CancellationToken cancellationToken)
    {
        return await _context.Templates
            .AsNoTracking()
            .Include(t => t.TemplateDepartments)
                .ThenInclude(td => td.Department)
            .Select(t => new TemplateDto
            {
                Id = t.Id,
                Name = t.Name,
                Description = t.Description,
                Departments = t.TemplateDepartments.Select(td => new DepartmentDto
                {
                    Id = td.Department.Id,
                    Name = td.Department.Name,
                    Description = td.Department.Description
                }).ToList()
            })
            .ToListAsync(cancellationToken);
    }
}
