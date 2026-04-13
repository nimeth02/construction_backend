using LevoHubBackend.Application.DTOs.Auth;
using LevoHubBackend.Application.Features.Auth.Commands.Login;
using LevoHubBackend.Application.Features.Auth.Commands.Register;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LevoHubBackend.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Authenticate a user and receive a JWT token.
    /// The token contains Role claims and Permission claims for policy-based authorization.
    /// </summary>
    [HttpPost("login")]
    public async Task<ActionResult<AuthResultDto>> Login([FromBody] LoginRequest request)
    {
        var result = await _mediator.Send(new LoginCommand
        {
            Email = request.Email,
            Password = request.Password
        });
        return Ok(result);
    }

    /// <summary>
    /// Register a new user account. After registration, use /login to get a token.
    /// </summary>
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        var result = await _mediator.Send(new RegisterCommand
        {
            Email = request.Email,
            Password = request.Password,
            FirstName = request.FirstName,
            LastName = request.LastName
        });
        return Ok(result);
    }
}
