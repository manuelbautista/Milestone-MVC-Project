using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCS.Common.Models
{
   public class TimestampDetailModel
    {
        public int ID { get; set; }
        public Nullable<int> ProjectId { get; set; }
        public string ColumnName { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }
}
