using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DCS.Common.Models
{
    public class ETOPlaybookModel : BaseModel
    {
        [Required(ErrorMessage = "Please enter ETO Playbook name.")]
        [Display(Name = "ETO Playbook")]
        public string Name { get; set; }

        public Nullable<bool> IsActive { get; set; }

        public DateTime Created { get; set; }

        public DateTime? Modified { get; set; }

        public int? CreatedBy { get; set; }

        public int? ModifiedBy { get; set; }

        
    }
}