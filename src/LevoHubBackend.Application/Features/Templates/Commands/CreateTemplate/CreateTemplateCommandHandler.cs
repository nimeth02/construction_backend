using LevoHubBackend.Application.Interfaces;
using LevoHubBackend.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LevoHubBackend.Application.Features.Templates.Commands.CreateTemplate;

public class CreateTemplateCommandHandler : IRequestHandler<CreateTemplateCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateTemplateCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateTemplateCommand request, CancellationToken cancellationToken)
    {
        await _context.BeginTransactionAsync(cancellationToken);

        try
        {
            var template = new Template
            {
                Name = request.Name,
                Description = request.Description
            };

            if (request.DepartmentIds != null && request.DepartmentIds.Any())
            {
                // Verify that all provided Department IDs exist in the database
                var existingDepartmentIds = await _context.Departments
                    .Where(d => request.DepartmentIds.Contains(d.Id))
                    .Select(d => d.Id)
                    .ToListAsync(cancellationToken);

                var invalidIds = request.DepartmentIds.Except(existingDepartmentIds).ToList();

                if (invalidIds.Any())
                {
                    throw new ArgumentException($"The following Department IDs are invalid or do not exist: {string.Join(", ", invalidIds)}");
                }

                foreach (var departmentId in existingDepartmentIds)
                {
                    template.TemplateDepartments.Add(new TemplateDepartment
                    {
                        DepartmentId = departmentId,
                        Template = template
                    });
                }
            }

            _context.Templates.Add(template);
            await _context.SaveChangesAsync(cancellationToken);

            await _context.CommitTransactionAsync(cancellationToken);

            return template.Id;
        }
        catch (Exception)
        {
            await _context.RollbackTransactionAsync(cancellationToken);
            throw;
        }
    }
}
