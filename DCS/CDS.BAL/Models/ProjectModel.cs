using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DCS.Common.Models
{
    public class ProjectModel : BaseModel
    {
        [Required(ErrorMessage = "Please enter project name.")]
        [Display(Name = "Project Name")]
        public string Name { get; set; }

        public string ProjectNumber { get; set; }


        [Display(Name = "EOPlaybook")]
        public int? EOPlaybook { get; set; }

        [Required(ErrorMessage = "Please select peer review.")]
        public int? PeerReview { get; set; }

        [Required(ErrorMessage = "Please select management approval.")]
        public int? ManagementApproval { get; set; }

        [Required(ErrorMessage = "Please select project owner name(ANTM).")]
        public int ProjectOwnerANTMId { get; set; }

        [Required(ErrorMessage = "Please select project owner name externam.")]
        public int ProjectOwnerExternalId { get; set; }

        public string Description { get; set; }

        public string ExecutiveStatus { get; set; }

        public string Accomplishments { get; set; }

        public string KeyRisksAndIssues { get; set; }

        public string Direction { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public int? CreatedBy { get; set; }

        public int? ModifiedBy { get; set; }
    }

}