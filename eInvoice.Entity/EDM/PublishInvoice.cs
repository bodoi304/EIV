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
    
    public partial class PublishInvoice
    {
        public int id { get; set; }
        public Nullable<int> ComId { get; set; }
        public Nullable<int> PublishID { get; set; }
        public Nullable<decimal> Quantity { get; set; }
        public Nullable<decimal> FromNo { get; set; }
        public Nullable<decimal> ToNo { get; set; }
        public Nullable<int> Status { get; set; }
        public Nullable<decimal> CurrentNo { get; set; }
        public string InvSerial { get; set; }
        public string InvSerialPrefix { get; set; }
        public string InvSerialSuffix { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }
        public Nullable<int> RegisterID { get; set; }
        public string RegisterName { get; set; }
        public string InvPattern { get; set; }
        public string InvCateName { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
    }
}
