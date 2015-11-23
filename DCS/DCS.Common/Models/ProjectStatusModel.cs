using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DCS.Common.Models
{
    public class ProjectStatusModel : BaseModel
    {
        public int ProjectId { get; set; }

        public int? ScopeId { get; set; }

        public int? ScheduleId { get; set; }

        public int? ResourcesId { get; set; }

        public int? FinancialId { get; set; }

        public int? ProjectStatusId { get; set; }
    }
}