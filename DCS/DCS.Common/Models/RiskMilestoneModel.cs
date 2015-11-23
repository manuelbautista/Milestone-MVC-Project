using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DCS.Common.Models
{
    public class RiskMilestoneModel : BaseModel
    {
        public string Name { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public int? CreatedBy { get; set; }

        public int? ModifiedBy { get; set; }
    }
}