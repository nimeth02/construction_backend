using LevoHubBackend.Application.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace LevoHubBackend.Application.Features.Departments.Commands.UpdateDepartment;

public class UpdateDepartmentCommandHandler : IRequestHandler<UpdateDepartmentCommand, bool>
{
    private readonly IApplicationDbContext _context;

    public UpdateDepartmentCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(UpdateDepartmentCommand request, CancellationToken cancellationToken)
    {
        var department = await _context.Departments.FindAsync(new object[] { request.Id }, cancellationToken);

        if (department == null)
        {
            return false;
        }
        
        if(request.Name != null){
            department.Name = request.Name;
        }

        if(request.Description != null){
            department.Description = request.Description;
        }

        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
