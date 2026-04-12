using LevoHubBackend.Application.Features.Roles.Commands.AssignPermission;
using LevoHubBackend.Application.Features.Roles.Commands.CreateRole;
using LevoHubBackend.Application.Features.Roles.Commands.DeleteRole;
using LevoHubBackend.Application.Features.Roles.Commands.RevokePermission;
using LevoHubBackend.Application.Features.Roles.Queries.GetRolePermissions;
using LevoHubBackend.Application.Features.Roles.Queries.GetRoles;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LevoHubBackend.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize] // All role management requires authentication
public class RolesController : ControllerBase
{
    private readonly IMediator _mediator;

    public RolesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>Get all roles</summary>
    [HttpGet]
    public async Task<ActionResult<List<RoleDto>>> GetRoles()
    {
        var roles = await _mediator.Send(new GetRolesQuery());
        return Ok(roles);
    }

    /// <summary>Create a new role</summary>
    [HttpPost]
    public async Task<ActionResult<Guid>> CreateRole(CreateRoleCommand command)
    {
        var id = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetRoles), new { id }, id);
    }

    /// <summary>Delete a role by ID</summary>
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteRole(Guid id)
    {
        var result = await _mediator.Send(new DeleteRoleCommand { RoleId = id });
        if (!result)
            return NotFound();
        return NoContent();
    }

    /// <summary>Get all permissions assigned to a role</summary>
    [HttpGet("{id:guid}/permissions")]
    public async Task<ActionResult<List<string>>> GetRolePermissions(Guid id)
    {
        var permissions = await _mediator.Send(new GetRolePermissionsQuery { RoleId = id });
        return Ok(permissions);
    }

    /// <summary>Assign a permission to a role</summary>
    [HttpPost("{id:guid}/permissions")]
    public async Task<IActionResult> AssignPermission(Guid id, [FromBody] PermissionRequest request)
    {
        await _mediator.Send(new AssignPermissionCommand { RoleId = id, Permission = request.Permission });
        return NoContent();
    }

    /// <summary>Revoke a permission from a role</summary>
    [HttpDelete("{id:guid}/permissions")]
    public async Task<IActionResult> RevokePermission(Guid id, [FromBody] PermissionRequest request)
    {
        await _mediator.Send(new RevokePermissionCommand { RoleId = id, Permission = request.Permission });
        return NoContent();
    }
}

/// <summary>Request body for assign/revoke permission endpoints</summary>
public record PermissionRequest(string Permission);
