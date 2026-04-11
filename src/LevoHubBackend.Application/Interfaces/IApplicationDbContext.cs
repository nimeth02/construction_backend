using LevoHubBackend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace LevoHubBackend.Application.Interfaces;

public interface IApplicationDbContext
{
    DbSet<User> Users { get; }
    DbSet<Role> Roles { get; }
    DbSet<Permission> Permissions { get; }
    DbSet<UserRole> UserRoles { get; }
    DbSet<RolePermission> RolePermissions { get; }
    DbSet<Department> Departments { get; }
    DbSet<JobTitle> JobTitles { get; }
    DbSet<AuditLog> AuditLogs { get; }
    DbSet<Stage> Stages { get; }
    DbSet<Template> Templates { get; }
    DbSet<TemplateDepartment> TemplateDepartments { get; }
    DbSet<Project> Projects { get; }
    DbSet<ProjectStage> ProjectStages { get; }
    DbSet<ProjectStageTask> ProjectStageTasks { get; }
    DbSet<ProjectStageEdge> ProjectStageEdges { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    Task BeginTransactionAsync(CancellationToken cancellationToken);
    Task CommitTransactionAsync(CancellationToken cancellationToken);
    Task RollbackTransactionAsync(CancellationToken cancellationToken);
}
