using LevoHubBackend.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace LevoHubBackend.Application.Features.Roles.Commands.RevokePermission;

public class RevokePermissionCommandHandler : IRequestHandler<RevokePermissionCommand, bool>
{
    private readonly RoleManager<Role> _roleManager;

    public RevokePermissionCommandHandler(RoleManager<Role> roleManager)
    {
        _roleManager = roleManager;
    }

    public async Task<bool> Handle(RevokePermissionCommand request, CancellationToken cancellationToken)
    {
        var role = await _roleManager.FindByIdAsync(request.RoleId.ToString());
        if (role == null)
            throw new InvalidOperationException($"Role with ID '{request.RoleId}' not found.");

        var existingClaims = await _roleManager.GetClaimsAsync(role);
        var claim = existingClaims.FirstOrDefault(c => c.Type == "Permission" && c.Value == request.Permission);
        if (claim == null)
            return true; // Idempotent — not assigned anyway

        var result = await _roleManager.RemoveClaimAsync(role, claim);
        return result.Succeeded;
    }
}
