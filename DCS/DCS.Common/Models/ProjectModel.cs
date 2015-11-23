using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DCS.Common.Models
{
    public class ProjectModel : BaseModel
    {
        [Required(ErrorMessage = "Please enter project name.")]
        [Display(Name = "Project Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter Project Number.")]
        public string ProjectNumber { get; set; }

        [UIHint("YesNo")]
        [Display(Name = "EOPlaybook")]
        public bool? EOPlaybook { get; set; }

        public int? EOPlaybookId { get; set; }

        [UIHint("YesNo")]
        [Required(ErrorMessage = "Please select peer review.")]
        public bool? PeerReview { get; set; }   

        [UIHint("YesNo")]
        [Required(ErrorMessage = "Please select management approval.")]
        public bool? ManagementApproval { get; set; }

        [Required(ErrorMessage = "Please select Anthem Owner name.")]
        public int ProjectOwnerANTMId { get; set; }

        [Required(ErrorMessage = "Please select Project Co-Owner Partner.")]
        public int ProjectOwnerExternalId { get; set; }

        public string Description { get; set; }

        public string ExecutiveStatus { get; set; }

        public string FundingAsk { get; set; }

        public string Accomplishments { get; set; }

        public string KeyRisksAndIssues { get; set; }

        public string Direction { get; set; }

        public bool? IsActive { get; set; }

        public DateTime Created { get; set; }

        public DateTime? Modified { get; set; }

        public int? CreatedBy { get; set; }

        public int? ModifiedBy { get; set; }

        public ProjectOwnerModel ProjectOwner { get; set; }

        public  ETOPlaybookModel ETOPlaybook { get; set; }

        public ICollection<ProjectStatusModel> ProjectStatus { get; set; }

        public string ProjectOwnerANTMIName { get; set; }

        public string ProjectOwnerExternalName { get; set; }

        public string ProjectStartDate { get; set; }

        public string ProjectEndDate { get; set; }
    }

}