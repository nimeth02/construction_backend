using LevoHubBackend.Domain.Enums;
using System;

namespace LevoHubBackend.Application.DTOs.Project;

public class ProjectDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string ClientName { get; set; } = string.Empty;
    public string ClientContactNumber { get; set; } = string.Empty;
    public string ClientId { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public bool IsActive { get; set; }
    public string Address { get; set; } = string.Empty;
    public DateTime? StartDate { get; set; }
    public DateTime? DeadlineDate { get; set; }
    public ProjectStatus Status { get; set; }
    public string StatusName => Status.ToString();
}
