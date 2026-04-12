using LevoHubBackend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace LevoHubBackend.Application.Interfaces;

public interface IApplicationDbContext
{
    DbSet<User> Users { get; set; }
    DbSet<Role> Roles { get; set; }
    DbSet<UserRole> UserRoles { get; set; }
    DbSet<Department> Departments { get; }
    DbSet<JobTitle> JobTitles { get; }
    DbSet<AuditLog> AuditLogs { get; }
    DbSet<Stage> Stages { get; }
    DbSet<Template> Templates { get; }
    DbSet<TemplateDepartment> TemplateDepartments { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    Task BeginTransactionAsync(CancellationToken cancellationToken);
    Task CommitTransactionAsync(CancellationToken cancellationToken);
    Task RollbackTransactionAsync(CancellationToken cancellationToken);
}
