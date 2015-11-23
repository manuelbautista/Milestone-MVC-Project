using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DCS.Common.Models
{
    public class ProjectScheduleModel : BaseModel
    {
        public int ProjectId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public decimal Complete { get; set; }

        public string StartDateString
        {
            get
            {
                return this.StartDate.ToShortDateString();
            }
        }

        public string EndDateString { get { return this.EndDate.Value.ToShortDateString(); } }
    }
}