using LevoHubBackend.Application.Interfaces;
using LevoHubBackend.Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace LevoHubBackend.Application.Features.Users.Commands.CreateUser;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Guid>
{
    private readonly IApplicationDbContext _context;
    private readonly IPasswordHasher _passwordHasher;

    public CreateUserCommandHandler(IApplicationDbContext context, IPasswordHasher passwordHasher)
    {
        _context = context;
        _passwordHasher = passwordHasher;
    }

    public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var passwordHash = _passwordHasher.HashPassword(request.Password);

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
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            PasswordHash = passwordHash,
            EmployeeCode = request.EmployeeCode,
            PhoneNumber = request.PhoneNumber,
            DepartmentId = request.DepartmentId,
            JobTitleId = request.JobTitleId,
            DateOfJoining = DateTime.UtcNow // Defaulting to now for creation
        };

        _context.Users.Add(user);

        // Assign Roles
        if (request.RoleIds != null)
        {
            foreach (var roleId in request.RoleIds)
            {
                var userRole = new UserRole
                {
                    User = user,
                    RoleId = roleId
                };
                _context.UserRoles.Add(userRole);
            }
        }

        await _context.SaveChangesAsync(cancellationToken);

        return user.Id;
    }
}
