using LevoHubBackend.Application.DTOs.Auth;
using LevoHubBackend.Application.Interfaces;
using LevoHubBackend.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace LevoHubBackend.Application.Features.Auth.Commands.Login;

public class LoginCommand : IRequest<AuthResultDto>
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}

public class LoginCommandHandler : IRequestHandler<LoginCommand, AuthResultDto>
{
    private readonly UserManager<User> _userManager;
    private readonly ITokenService _tokenService;
    private readonly RoleManager<Role> _roleManager;
    private readonly IConfiguration _configuration;

    public LoginCommandHandler(UserManager<User> userManager, ITokenService tokenService, RoleManager<Role> roleManager, IConfiguration configuration)
    {
        _userManager = userManager;
        _tokenService = tokenService;
        _roleManager = roleManager;
        _configuration = configuration;
    }

    public async Task<AuthResultDto> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(request.Email)
            ?? throw new UnauthorizedAccessException("Invalid email or password.");

        var passwordValid = await _userManager.CheckPasswordAsync(user, request.Password);
        if (!passwordValid)
            throw new UnauthorizedAccessException("Invalid email or password.");

        if (!user.IsActive)
            throw new UnauthorizedAccessException("Your account is disabled. Please contact an administrator.");

        // Get roles the user belongs to
        var roles = await _userManager.GetRolesAsync(user);

        // Get all permission claims directly assigned to the user
        var userClaims = await _userManager.GetClaimsAsync(user);
        var permissionClaims = userClaims.ToList();

        // Also gather claims from the roles (this is where permission claims live)
        foreach (var roleName in roles)
        {
            var role = await _roleManager.FindByNameAsync(roleName);
            if (role != null)
            {
                var roleClaims = await _roleManager.GetClaimsAsync(role);
                permissionClaims.AddRange(roleClaims);
            }
        }
        var expiryMinutes = int.Parse(_configuration["JwtSettings:ExpiryMinutes"] ?? "60");

        return new AuthResultDto
        {
            AccessToken = _tokenService.GenerateToken(user, roles, permissionClaims),
            ExpiresIn = expiryMinutes * 60,
            User = new AuthUserDto
            {
                Id = user.Id.ToString(),
                Email = user.Email!,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Roles = roles
            }
        };
    }
}
