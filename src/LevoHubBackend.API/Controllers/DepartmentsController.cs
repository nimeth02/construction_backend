using LevoHubBackend.Application.DTOs.Department;
using LevoHubBackend.Application.Features.Departments.Commands.CreateDepartment;
using LevoHubBackend.Application.Features.Departments.Commands.DeleteDepartment;
using LevoHubBackend.Application.Features.Departments.Commands.UpdateDepartment;
using LevoHubBackend.Application.Features.Departments.Queries.GetDepartmentById;
using LevoHubBackend.Application.Features.Departments.Queries.GetDepartments;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LevoHubBackend.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DepartmentsController : ControllerBase
{
    private readonly IMediator _mediator;

    public DepartmentsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<List<DepartmentDto>>> GetDepartments()
    {
        return await _mediator.Send(new GetDepartmentsQuery());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<DepartmentDto>> GetDepartmentById(int id)
    {
        var department = await _mediator.Send(new GetDepartmentByIdQuery { Id = id });
        if (department == null)
        {
            return NotFound();
        }
        return department;
    }

    [HttpPost]
    public async Task<ActionResult<int>> CreateDepartment(CreateDepartmentCommand command)
    {
        var id = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetDepartmentById), new { id }, id);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateDepartment(int id, UpdateDepartmentCommand command)
    {
        command.Id = id;

        var result = await _mediator.Send(command);
        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDepartment(int id)
    {
        var result = await _mediator.Send(new DeleteDepartmentCommand { Id = id });
        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
