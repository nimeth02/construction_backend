using LevoHubBackend.Application.DTOs.Department;
using MediatR;
using System.Collections.Generic;

namespace LevoHubBackend.Application.Features.Departments.Queries.GetDepartments;

public class GetDepartmentsQuery : IRequest<List<DepartmentDto>>
{
}
