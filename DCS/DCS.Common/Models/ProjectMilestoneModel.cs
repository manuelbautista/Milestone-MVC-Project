using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DCS.Common.Models
{
    public class ProjectMilestoneModel : BaseModel
    {
        public int ProjectId { get; set; }

        [Required(ErrorMessage = "Please enter Milestone name.")]
        [Display(Name = "Milestone name")]
        public string Name { get; set; }

        public int MilestoneStatus { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public decimal Complete { get; set; }

        public int? RiskMilestoneId { get; set; }

        public Nullable<DateTime> ModifiedDate { get; set; }

        public string StartDateString { get { return string.Format("{0:MM/dd/yyyy}", this.StartDate); } }
    }
}