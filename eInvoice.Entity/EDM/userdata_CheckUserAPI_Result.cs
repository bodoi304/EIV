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
    
    public partial class userdata_CheckUserAPI_Result
    {
        public int userid { get; set; }
        public string username { get; set; }
        public Nullable<int> Type { get; set; }
        public string password { get; set; }
        public Nullable<int> PasswordFormat { get; set; }
        public string PasswordSalt { get; set; }
        public string email { get; set; }
        public string PasswordQuestion { get; set; }
        public string PasswordAnswer { get; set; }
        public Nullable<bool> IsApproved { get; set; }
        public System.DateTime CreateDate { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<int> FailedPassAttemptCount { get; set; }
        public string GroupName { get; set; }
        public string FullName { get; set; }
        public string Serials { get; set; }
        public string Position { get; set; }
        public string TaxCode { get; set; }
    }
}
