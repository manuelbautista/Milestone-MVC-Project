using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCS.Common.Models
{
   public  class ProjectMultiEmailModel
    {


        public int ProjectId { get; set; }

        [Required(ErrorMessage = "Please enter Email.")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        public int ID { get; set; }
    }
}
