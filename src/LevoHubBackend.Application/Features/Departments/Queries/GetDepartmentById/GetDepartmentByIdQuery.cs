using LevoHubBackend.Application.DTOs.Department;
using MediatR;

namespace LevoHubBackend.Application.Features.Departments.Queries.GetDepartmentById;

public class GetDepartmentByIdQuery : IRequest<DepartmentDto?>
{
    public int Id { get; set; }
}
