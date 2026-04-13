using LevoHubBackend.Application.Interfaces;
using LevoHubBackend.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LevoHubBackend.Application.Features.Users.Commands.CreateUser;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Guid>
{
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<Role> _roleManager;
    private readonly IApplicationDbContext _context;

    public CreateUserCommandHandler(UserManager<User> userManager, RoleManager<Role> roleManager, IApplicationDbContext context)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _context = context;
    }

    public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        // Derive DepartmentId from JobTitle if provided
        if (request.JobTitleId.HasValue)
        {
            var jobTitle = await _context.JobTitles.FindAsync(new object[] { request.JobTitleId.Value }, cancellationToken);
            if (jobTitle != null)
            {
                request.DepartmentId = jobTitle.DepartmentId;
            }
        }

        var user = new User
        {
            Id = Guid.NewGuid(),
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            UserName = request.Email, // Identity uses UserName, often set to Email
            EmployeeCode = request.EmployeeCode,
            PhoneNumber = request.PhoneNumber,
            DepartmentId = request.DepartmentId,
            JobTitleId = request.JobTitleId,
            DateOfJoining = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow,
            IsActive = true
        };

        var result = await _userManager.CreateAsync(user, request.Password);

        if (!result.Succeeded)
        {
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            throw new Exception($"User creation failed: {errors}");
        }

        // Assign Roles
        if (request.RoleIds != null && request.RoleIds.Any())
        {
            foreach (var roleId in request.RoleIds)
            {
                var role = await _roleManager.FindByIdAsync(roleId.ToString());
                if (role != null)
                {
                    await _userManager.AddToRoleAsync(user, role.Name!);
                }
            }
        }

        return user.Id;
    }
}
