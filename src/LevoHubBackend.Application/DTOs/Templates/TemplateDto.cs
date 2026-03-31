using LevoHubBackend.Application.DTOs.Department;
using System.Collections.Generic;

namespace LevoHubBackend.Application.DTOs.Templates;

public class TemplateDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public List<DepartmentDto> Departments { get; set; } = new();
}
