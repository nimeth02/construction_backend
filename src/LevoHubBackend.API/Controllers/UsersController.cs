using LevoHubBackend.Application.Common.Authorization;
using LevoHubBackend.Application.Features.Users.Commands.CreateUser;
using LevoHubBackend.Application.Features.Users.Commands.UpdateUserRoles;
using LevoHubBackend.Application.Features.Users.Queries.GetUsersWithRoles;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LevoHubBackend.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;

    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Authorize(Policy = Permissions.Users.View)]
    public async Task<ActionResult<List<UserWithRolesDto>>> GetUsers()
    {
        var users = await _mediator.Send(new GetUsersWithRolesQuery());
        return Ok(users);
    }

    [HttpPost]
    [Authorize(Policy = Permissions.Users.Create)]
    public async Task<ActionResult<Guid>> CreateUser(CreateUserCommand command)
    {
        var userId = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetUsers), new { id = userId }, userId);
    }

    [HttpPut("{id:guid}/roles")]
    [Authorize(Policy = Permissions.Users.Edit)]
    public async Task<IActionResult> UpdateUserRoles(Guid id, [FromBody] List<string> roles)
    {
        var result = await _mediator.Send(new UpdateUserRolesCommand { UserId = id, Roles = roles });
        if (!result)
            return NotFound();
        return NoContent();
    }
}
