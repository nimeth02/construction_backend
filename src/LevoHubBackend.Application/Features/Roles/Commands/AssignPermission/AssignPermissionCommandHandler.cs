using LevoHubBackend.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace LevoHubBackend.Application.Features.Roles.Commands.AssignPermission;

public class AssignPermissionCommandHandler : IRequestHandler<AssignPermissionCommand, bool>
{
    private readonly RoleManager<Role> _roleManager;

    public AssignPermissionCommandHandler(RoleManager<Role> roleManager)
    {
        _roleManager = roleManager;
    }

    public async Task<bool> Handle(AssignPermissionCommand request, CancellationToken cancellationToken)
    {
        var role = await _roleManager.FindByIdAsync(request.RoleId.ToString());
        if (role == null)
            throw new InvalidOperationException($"Role with ID '{request.RoleId}' not found.");

        var existingClaims = await _roleManager.GetClaimsAsync(role);
        var alreadyHas = existingClaims.Any(c => c.Type == "Permission" && c.Value == request.Permission);
        if (alreadyHas)
            return true; // Idempotent — already assigned

        var result = await _roleManager.AddClaimAsync(role, new Claim("Permission", request.Permission));
        return result.Succeeded;
    }
}
