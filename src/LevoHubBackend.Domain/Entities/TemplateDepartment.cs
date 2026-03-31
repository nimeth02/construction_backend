namespace LevoHubBackend.Domain.Entities;

public class TemplateDepartment
{
    public int TemplateId { get; set; }
    public Template Template { get; set; } = null!;

    public int DepartmentId { get; set; }
    public Department Department { get; set; } = null!;
}
