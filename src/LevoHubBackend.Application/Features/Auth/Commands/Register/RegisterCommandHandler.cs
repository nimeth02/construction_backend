using LevoHubBackend.Application.DTOs.Auth;
using LevoHubBackend.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LevoHubBackend.Application.Common.Authorization;
namespace LevoHubBackend.Application.Features.Auth.Commands.Register;

public class RegisterCommand : IRequest<AuthResultDto>
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
}

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, AuthResultDto>
{
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<Role> _roleManager;

    public RegisterCommandHandler(UserManager<User> userManager, RoleManager<Role> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task<AuthResultDto> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var existingUser = await _userManager.FindByEmailAsync(request.Email);
        if (existingUser != null)
            throw new ArgumentException($"An account with the email '{request.Email}' already exists.");

        var user = new User
        {
            Id = Guid.NewGuid(),
            Email = request.Email,
            UserName = request.Email,
            FirstName = request.FirstName,
            LastName = request.LastName,
            CreatedAt = DateTime.UtcNow,
            IsActive = true
        };

        var result = await _userManager.CreateAsync(user, request.Password);

        if (!result.Succeeded)
        {
            var errors = string.Join("; ", result.Errors.Select(e => e.Description));
            throw new ArgumentException($"Registration failed: {errors}");
        }

        // --- Bootstrap Logic ---
        var isFirstUser = await _userManager.Users.CountAsync() == 1;

        if (isFirstUser)
        {
            // Create and configure Super Admin role
            if (!await _roleManager.RoleExistsAsync("Super Admin"))
                await _roleManager.CreateAsync(new Role { Id = Guid.NewGuid(), Name = "Super Admin", Description = "Full access role" });

            var saRole = await _roleManager.FindByNameAsync("Super Admin");
            var allPermissions = new[] {
                Permissions.Templates.View, Permissions.Templates.Create, Permissions.Templates.Edit, Permissions.Templates.Delete,
                Permissions.Departments.View, Permissions.Departments.Create, Permissions.Departments.Edit, Permissions.Departments.Delete,
                Permissions.Users.View, Permissions.Users.Create, Permissions.Users.Edit, Permissions.Users.Delete,
                Permissions.Stages.View, Permissions.Stages.Create, Permissions.Stages.Edit, Permissions.Stages.Delete
            };
            
            var existingClaims = await _roleManager.GetClaimsAsync(saRole);
            foreach(var perm in allPermissions)
            {
                if (!existingClaims.Any(c => c.Type == "Permission" && c.Value == perm))
                    await _roleManager.AddClaimAsync(saRole, new Claim("Permission", perm));
            }
            await _userManager.AddToRoleAsync(user, "Super Admin");

            // Also initialize User role for future users
            if (!await _roleManager.RoleExistsAsync("User"))
                await _roleManager.CreateAsync(new Role { Id = Guid.NewGuid(), Name = "User", Description = "Basic user access" });
                
            var userRole = await _roleManager.FindByNameAsync("User");
            if (userRole != null)
            {
                var userPermissions = new[] {
                    Permissions.Templates.View, Permissions.Departments.View, Permissions.Users.View, Permissions.Stages.View
                };
                var userExistingClaims = await _roleManager.GetClaimsAsync(userRole);
                foreach(var perm in userPermissions)
                {
                    if (!userExistingClaims.Any(c => c.Type == "Permission" && c.Value == perm))
                        await _roleManager.AddClaimAsync(userRole, new Claim("Permission", perm));
                }
            }
        }
        else
        {
            // Ensure User role exists (just in case) and assign to user
            if (!await _roleManager.RoleExistsAsync("User"))
                await _roleManager.CreateAsync(new Role { Id = Guid.NewGuid(), Name = "User", Description = "Basic user access" });

            await _userManager.AddToRoleAsync(user, "User");
        }
        // --- End Bootstrap Logic ---

        var assignedRoles = await _userManager.GetRolesAsync(user);

        return new AuthResultDto
        {
            AccessToken = string.Empty, // Token not returned on register; user must login
            ExpiresIn = 0,
            User = new AuthUserDto
            {
                Id = user.Id.ToString(),
                Email = user.Email!,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Roles = assignedRoles
            }
        };
    }
}
