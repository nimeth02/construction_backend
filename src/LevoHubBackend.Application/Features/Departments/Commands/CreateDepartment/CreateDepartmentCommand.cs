using MediatR;

namespace LevoHubBackend.Application.Features.Departments.Commands.CreateDepartment;

public class CreateDepartmentCommand : IRequest<int>
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
}
