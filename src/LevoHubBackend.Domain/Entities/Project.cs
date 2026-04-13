using LevoHubBackend.Domain.Common;
using LevoHubBackend.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace LevoHubBackend.Domain.Entities
{
    public class Project : AuditableEntity<int>
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ClientName { get; set; } = string.Empty;
        public string ClientContactNumber { get; set; } = string.Empty;
        public string ClientId { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public DateTime? StartDate { get; set; }
        public DateTime? DeadlineDate { get; set; }
        public ProjectStatus Status { get; set; } = ProjectStatus.Active;

        public ICollection<ProjectStage> ProjectStages { get; set; } = new List<ProjectStage>();
        public ICollection<ProjectStageEdge> ProjectStageEdges { get; set; } = new List<ProjectStageEdge>();
    }
}
