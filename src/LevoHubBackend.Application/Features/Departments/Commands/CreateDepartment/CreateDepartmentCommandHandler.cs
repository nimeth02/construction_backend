using LevoHubBackend.Application.Interfaces;
using LevoHubBackend.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace LevoHubBackend.Application.Features.Departments.Commands.CreateDepartment;

public class CreateDepartmentCommandHandler : IRequestHandler<CreateDepartmentCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateDepartmentCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateDepartmentCommand request, CancellationToken cancellationToken)
    {
        var department = new Department
        {
            Name = request.Name,
            Description = request.Description
        };

        _context.Departments.Add(department);
        await _context.SaveChangesAsync(cancellationToken);

        return department.Id;
    }
}
