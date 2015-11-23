using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCS.Common.DTO
{
    public class ETOPlaybookDTO
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public Nullable<bool> IsActive { get; set; }

        public System.DateTime Created { get; set; }

        public Nullable<System.DateTime> Modified { get; set; }

        public Nullable<int> CreatedBy { get; set; }

        public Nullable<int> ModifiedBy { get; set; }
    }
}
