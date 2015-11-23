using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DCSProject.Models
{
    public class ProjectModel
    {
        public int Id { get; set; }

        public string ProjectName { get; set; }

        public string ProjectNumber { get; set; }

        public bool EOPlaybook { get; set; }

        public bool PeerReview { get; set; }

        public bool ManagerAppr { get; set; }

        public int? ProjectOwnerANTM { get; set; }

        public int? ProjectOwnerExternal { get; set; }

        public string Description { get; set; }

        public string ExecutiveStatus { get; set; }

        public string Accomplishments { get; set; }

        public string KeyRisksAndIssues { get; set; }

        public string Direction  { get; set; }

        public DateTime? CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }
    }

    public class ProjectMilestones
    {
        public int Id { get; set; }

        public int ProjectId { get; set; }

        public string MilestoneNumber { get; set; }

        public string MilestoneName { get; set; }

        public DateTime? MilestoneStart { get; set; }

        public DateTime? MilestoneEnd { get; set; }

        public string MilestoneComplete { get; set; }

        public int? MilestoneRisk { get; set; }
    }
}