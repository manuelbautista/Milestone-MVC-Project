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
    
    public partial class usp_GetProjectSummaryDetails_Result2
    {
        public int ID { get; set; }
        public string ProjectName { get; set; }
        public string FullName { get; set; }
        public Nullable<System.DateTime> ProjectStartDate { get; set; }
        public Nullable<System.DateTime> ProjectEndDate { get; set; }
        public Nullable<decimal> ProjectComplete { get; set; }
        public string ProjectDescription { get; set; }
        public string ExecutiveStatus { get; set; }
        public string Accomplishments { get; set; }
        public string KeyRisksAndIssues { get; set; }
        public string Direction { get; set; }
        public Nullable<int> ScopeId { get; set; }
        public Nullable<int> ResourcesId { get; set; }
        public Nullable<int> FinancialId { get; set; }
        public Nullable<int> ProjectStatusId { get; set; }
        public Nullable<int> ScheduleId { get; set; }
    }
}
