//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DCS.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class ETOPlaybook
    {
        public ETOPlaybook()
        {
            this.Projects = new HashSet<Project>();
            this.Projects1 = new HashSet<Project>();
        }
    
        public int ID { get; set; }
        public string Name { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public System.DateTime Created { get; set; }
        public Nullable<System.DateTime> Modified { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
    
        public virtual ICollection<Project> Projects { get; set; }
        public virtual ICollection<Project> Projects1 { get; set; }
    }
}
