using LevoHubBackend.Application.Interfaces;
using LevoHubBackend.Domain.Entities;
using LevoHubBackend.Domain.Common;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace LevoHubBackend.Infrastructure.Data;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<User> Users => Set<User>();
    public DbSet<Role> Roles => Set<Role>();
    public DbSet<Permission> Permissions => Set<Permission>();
    public DbSet<UserRole> UserRoles => Set<UserRole>();
    public DbSet<RolePermission> RolePermissions => Set<RolePermission>();
    public DbSet<Department> Departments => Set<Department>();
    public DbSet<JobTitle> JobTitles => Set<JobTitle>();
    public DbSet<AuditLog> AuditLogs => Set<AuditLog>();
    public DbSet<Stage> Stages => Set<Stage>();
    public DbSet<Template> Templates => Set<Template>();
    public DbSet<TemplateDepartment> TemplateDepartments => Set<TemplateDepartment>();
    public DbSet<Project> Projects => Set<Project>();
    public DbSet<ProjectStage> ProjectStages => Set<ProjectStage>();
    public DbSet<ProjectStageTask> ProjectStageTasks => Set<ProjectStageTask>();
    public DbSet<ProjectStageEdge> ProjectStageEdges => Set<ProjectStageEdge>();

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await base.SaveChangesAsync(cancellationToken);
    }

    public async Task BeginTransactionAsync(CancellationToken cancellationToken)
    {
        await base.Database.BeginTransactionAsync(cancellationToken);
    }

    public async Task CommitTransactionAsync(CancellationToken cancellationToken)
    {
        if (base.Database.CurrentTransaction != null)
        {
            await base.Database.CurrentTransaction.CommitAsync(cancellationToken);
        }
    }

    public async Task RollbackTransactionAsync(CancellationToken cancellationToken)
    {
        if (base.Database.CurrentTransaction != null)
        {
            await base.Database.CurrentTransaction.RollbackAsync(cancellationToken);
        }
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        // Configure Many-to-Many for UserRole
        builder.Entity<UserRole>()
            .HasKey(ur => new { ur.UserId, ur.RoleId });

        builder.Entity<UserRole>()
            .HasOne(ur => ur.User)
            .WithMany(u => u.UserRoles)
            .HasForeignKey(ur => ur.UserId);

        builder.Entity<UserRole>()
            .HasOne(ur => ur.Role)
            .WithMany(r => r.UserRoles)
            .HasForeignKey(ur => ur.RoleId);

        // Configure Many-to-Many for RolePermission
        builder.Entity<RolePermission>()
            .HasKey(rp => new { rp.RoleId, rp.PermissionId });

        builder.Entity<RolePermission>()
            .HasOne(rp => rp.Role)
            .WithMany(r => r.RolePermissions)
            .HasForeignKey(rp => rp.RoleId);

        builder.Entity<RolePermission>()
            .HasOne(rp => rp.Permission)
            .WithMany(p => p.RolePermissions)
            .HasForeignKey(rp => rp.PermissionId);
        
        // Configure Many-to-Many for TemplateDepartment
        builder.Entity<TemplateDepartment>()
            .HasKey(td => new { td.TemplateId, td.DepartmentId });

        builder.Entity<TemplateDepartment>()
            .HasOne(td => td.Template)
            .WithMany(t => t.TemplateDepartments)
            .HasForeignKey(td => td.TemplateId);

        builder.Entity<TemplateDepartment>()
            .HasOne(td => td.Department)
            .WithMany(d => d.TemplateDepartments)
            .HasForeignKey(td => td.DepartmentId);
        
        // Configure ProjectStage (Many-to-Many with Payload)
        builder.Entity<ProjectStage>()
            .HasOne(ps => ps.Project)
            .WithMany(p => p.ProjectStages)
            .HasForeignKey(ps => ps.ProjectId);

        builder.Entity<ProjectStage>()
            .HasOne(ps => ps.Stage)
            .WithMany(s => s.ProjectStages)
            .HasForeignKey(ps => ps.StageId);

        // Configure ProjectStageTask
        builder.Entity<ProjectStageTask>()
            .HasOne(pst => pst.ProjectStage)
            .WithMany(ps => ps.ProjectStageTasks)
            .HasForeignKey(pst => pst.ProjectStageId);

        builder.Entity<ProjectStageTask>()
            .HasOne(pst => pst.AssignedUser)
            .WithMany(u => u.AssignedTasks)
            .HasForeignKey(pst => pst.UserId);

        // Configure ProjectStageEdge
        builder.Entity<ProjectStageEdge>()
            .HasOne(pse => pse.Project)
            .WithMany(p => p.ProjectStageEdges)
            .HasForeignKey(pse => pse.ProjectId);

        builder.Entity<ProjectStageEdge>()
            .HasOne(pse => pse.FromProjectStage)
            .WithMany(ps => ps.FromEdges)
            .HasForeignKey(pse => pse.FromProjectStageId)
            .OnDelete(DeleteBehavior.Restrict); // Avoid multiple cascade paths

        builder.Entity<ProjectStageEdge>()
            .HasOne(pse => pse.ToProjectStage)
            .WithMany(ps => ps.ToEdges)
            .HasForeignKey(pse => pse.ToProjectStageId)
            .OnDelete(DeleteBehavior.Restrict); // Avoid multiple cascade paths
        
        // Removed UserProfile One-to-One as it was deleted
    }
}
