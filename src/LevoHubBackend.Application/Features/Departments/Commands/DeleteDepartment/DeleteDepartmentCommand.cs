using MediatR;

namespace LevoHubBackend.Application.Features.Departments.Commands.DeleteDepartment;

public class DeleteDepartmentCommand : IRequest<bool>
{
    public int Id { get; set; }
}
