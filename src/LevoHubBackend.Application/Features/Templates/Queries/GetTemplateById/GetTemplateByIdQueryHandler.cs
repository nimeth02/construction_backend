using LevoHubBackend.Application.DTOs.Department;
using LevoHubBackend.Application.DTOs.Templates;
using LevoHubBackend.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LevoHubBackend.Application.Features.Templates.Queries.GetTemplateById;

public class GetTemplateByIdQueryHandler : IRequestHandler<GetTemplateByIdQuery, TemplateDto>
{
    private readonly IApplicationDbContext _context;

    public GetTemplateByIdQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<TemplateDto> Handle(GetTemplateByIdQuery request, CancellationToken cancellationToken)
    {
        var template = await _context.Templates
            .AsNoTracking()
            .Include(t => t.TemplateDepartments)
                .ThenInclude(td => td.Department)
            .FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken);

        if (template == null)
        {
            return null!;
        }

        return new TemplateDto
        {
            Id = template.Id,
            Name = template.Name,
            Description = template.Description,
            Departments = template.TemplateDepartments.Select(td => new DepartmentDto
            {
                Id = td.Department.Id,
                Name = td.Department.Name,
                Description = td.Department.Description
            }).ToList()
        };
    }
}
