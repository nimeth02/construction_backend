using System;

namespace LevoHubBackend.Application.DTOs.Dashboard;

public class UpcomingDeadlineDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public DateTime? DeadlineDate { get; set; }
    
    public int? OverdueDays 
    { 
        get 
        {
            if (!DeadlineDate.HasValue) return null;
            var diff = (DateTime.Now - DeadlineDate.Value).Days;
            return diff;
        }
    }

    public string OverdueText 
    {
        get 
        {
            if (!DeadlineDate.HasValue) return string.Empty;
            var days = OverdueDays ?? 0;
            if (days > 0) return $"{days}d overdue";
            if (days == 0) return "Due today";
            return $"Due in {Math.Abs(days)}d";
        }
    }
}
