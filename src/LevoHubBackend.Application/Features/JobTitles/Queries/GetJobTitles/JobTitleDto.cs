namespace LevoHubBackend.Application.Features.JobTitles.Queries.GetJobTitles;

public class JobTitleDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Level { get; set; }
    public string? Description { get; set; }
    public int DepartmentId { get; set; }
    public string? DepartmentName { get; set; }
}
