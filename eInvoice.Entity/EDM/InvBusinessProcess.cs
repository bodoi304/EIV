//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace eInvoice.Entity.EDM
{
    using System;
    using System.Collections.Generic;
    
    public partial class InvBusinessProcess
    {
        public int InvBusinessProcessID { get; set; }
        public int InvID { get; set; }
        public int BusinessID { get; set; }
        public int DepartmentID { get; set; }
        public int BusinessDepartmentID { get; set; }
        public int StepNo { get; set; }
        public string JobType { get; set; }
        public Nullable<int> UserID { get; set; }
        public Nullable<bool> StatusApprove { get; set; }
        public string Comment { get; set; }
        public Nullable<int> InvBPType { get; set; }
    }
}
