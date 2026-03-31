using MediatR;

namespace LevoHubBackend.Application.Features.Departments.Commands.UpdateDepartment;

public class UpdateDepartmentCommand : IRequest<bool>
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
}
