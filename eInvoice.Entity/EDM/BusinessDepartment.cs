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
    
    public partial class BusinessDepartment
    {
        public int BusinessDepartmentID { get; set; }
        public string BDCode { get; set; }
        public string BDName { get; set; }
        public int BusinessID { get; set; }
        public int DepartmentID { get; set; }
        public int StepNo { get; set; }
        public int UserID { get; set; }
        public bool IsDelete { get; set; }
        public int CreatedUserID { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<int> UdpatedUserID { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public string JobType { get; set; }
        public int RoleID { get; set; }
        public Nullable<int> WareHouseID { get; set; }
        public Nullable<int> Type { get; set; }
        public Nullable<int> UserDelegationID { get; set; }
    }
}
