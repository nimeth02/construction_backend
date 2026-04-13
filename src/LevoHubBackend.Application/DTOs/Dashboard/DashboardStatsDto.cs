namespace LevoHubBackend.Application.DTOs.Dashboard;

public class DashboardStatsDto
{
    public int TotalProjects { get; set; }
    public int TotalProjectsThisMonthChange { get; set; }
    
    public int ActiveProjects { get; set; }
    
    public int PendingTasks { get; set; }
    
    public int CompletedProjects { get; set; }
    public int CompletedProjectsThisWeekChange { get; set; }
}
