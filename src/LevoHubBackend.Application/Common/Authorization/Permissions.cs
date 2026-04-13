namespace LevoHubBackend.Application.Common.Authorization;

/// <summary>
/// All available permission claim values in the system.
/// These are stored as IdentityRoleClaim values and embedded in the JWT.
/// </summary>
public static class Permissions
{
    public static class Templates
    {
        public const string View   = "Permissions.Templates.View";
        public const string Create = "Permissions.Templates.Create";
        public const string Edit   = "Permissions.Templates.Edit";
        public const string Delete = "Permissions.Templates.Delete";
    }

    public static class Departments
    {
        public const string View   = "Permissions.Departments.View";
        public const string Create = "Permissions.Departments.Create";
        public const string Edit   = "Permissions.Departments.Edit";
        public const string Delete = "Permissions.Departments.Delete";
    }

    public static class Users
    {
        public const string View   = "Permissions.Users.View";
        public const string Create = "Permissions.Users.Create";
        public const string Edit   = "Permissions.Users.Edit";
        public const string Delete = "Permissions.Users.Delete";
    }

    public static class Stages
    {
        public const string View   = "Permissions.Stages.View";
        public const string Create = "Permissions.Stages.Create";
        public const string Edit   = "Permissions.Stages.Edit";
        public const string Delete = "Permissions.Stages.Delete";
    }
}
