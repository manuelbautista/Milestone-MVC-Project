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


        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public decimal Complete { get; set; }

        public int? RiskMilestoneId { get; set; }
    }
}