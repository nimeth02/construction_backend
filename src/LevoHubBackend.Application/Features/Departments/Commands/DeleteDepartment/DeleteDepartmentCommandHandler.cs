using LevoHubBackend.Application.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace LevoHubBackend.Application.Features.Departments.Commands.DeleteDepartment;

public class DeleteDepartmentCommandHandler : IRequestHandler<DeleteDepartmentCommand, bool>
{
    private readonly IApplicationDbContext _context;

    public DeleteDepartmentCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(DeleteDepartmentCommand request, CancellationToken cancellationToken)
    {
        var department = await _context.Departments.FindAsync(new object[] { request.Id }, cancellationToken);

        if (department == null)
        {
            return false;
        }

        _context.Departments.Remove(department);
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
