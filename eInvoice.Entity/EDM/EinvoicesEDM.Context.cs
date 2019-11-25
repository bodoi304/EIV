﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class HDDT_pvoilEntities : DbContext
    {
        public HDDT_pvoilEntities()
            : base("name=HDDT_pvoilEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<ApiUserAccess> ApiUserAccesses { get; set; }
        public virtual DbSet<BusinessDepartment> BusinessDepartments { get; set; }
        public virtual DbSet<UserDepartment> UserDepartments { get; set; }
        public virtual DbSet<AccountingAccount> AccountingAccounts { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<ProductInv> ProductInvs { get; set; }
        public virtual DbSet<Company> Companies { get; set; }
        public virtual DbSet<userdata> userdatas { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Currency> Currencies { get; set; }
        public virtual DbSet<PublishInvoice> PublishInvoices { get; set; }
        public virtual DbSet<PVOILInvoice> PVOILInvoices { get; set; }
        public virtual DbSet<Warehouse> Warehouses { get; set; }
        public virtual DbSet<InvTemplate> InvTemplates { get; set; }
        public virtual DbSet<AdjustInv> AdjustInvs { get; set; }
        public virtual DbSet<Business> Businesses { get; set; }
        public virtual DbSet<InvBusinessProcess> InvBusinessProcesses { get; set; }
    
        public virtual ObjectResult<SyncCategory_DiemXuat_Result> SyncCategory_DiemXuat(string username, string taxCode)
        {
            var usernameParameter = username != null ?
                new ObjectParameter("username", username) :
                new ObjectParameter("username", typeof(string));
    
            var taxCodeParameter = taxCode != null ?
                new ObjectParameter("TaxCode", taxCode) :
                new ObjectParameter("TaxCode", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SyncCategory_DiemXuat_Result>("SyncCategory_DiemXuat", usernameParameter, taxCodeParameter);
        }
    
        public virtual ObjectResult<SyncCategory_NghiepVu_Result> SyncCategory_NghiepVu(string username, string taxCode)
        {
            var usernameParameter = username != null ?
                new ObjectParameter("username", username) :
                new ObjectParameter("username", typeof(string));
    
            var taxCodeParameter = taxCode != null ?
                new ObjectParameter("TaxCode", taxCode) :
                new ObjectParameter("TaxCode", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SyncCategory_NghiepVu_Result>("SyncCategory_NghiepVu", usernameParameter, taxCodeParameter);
        }
    
        public virtual ObjectResult<Warehouse_SelectByTaxCode_Result> Warehouse_SelectByTaxCode(string taxCode)
        {
            var taxCodeParameter = taxCode != null ?
                new ObjectParameter("TaxCode", taxCode) :
                new ObjectParameter("TaxCode", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Warehouse_SelectByTaxCode_Result>("Warehouse_SelectByTaxCode", taxCodeParameter);
        }
    
        public virtual ObjectResult<InvTemplate_GetTemplateInvoice_Result> InvTemplate_GetTemplateInvoice(string pattern, string taxCode)
        {
            var patternParameter = pattern != null ?
                new ObjectParameter("pattern", pattern) :
                new ObjectParameter("pattern", typeof(string));
    
            var taxCodeParameter = taxCode != null ?
                new ObjectParameter("TaxCode", taxCode) :
                new ObjectParameter("TaxCode", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<InvTemplate_GetTemplateInvoice_Result>("InvTemplate_GetTemplateInvoice", patternParameter, taxCodeParameter);
        }
    
        public virtual ObjectResult<userdata_CheckUserAPI_Result> userdata_CheckUserAPI(string username, string taxCode, Nullable<int> type)
        {
            var usernameParameter = username != null ?
                new ObjectParameter("username", username) :
                new ObjectParameter("username", typeof(string));
    
            var taxCodeParameter = taxCode != null ?
                new ObjectParameter("TaxCode", taxCode) :
                new ObjectParameter("TaxCode", typeof(string));
    
            var typeParameter = type.HasValue ?
                new ObjectParameter("Type", type) :
                new ObjectParameter("Type", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<userdata_CheckUserAPI_Result>("userdata_CheckUserAPI", usernameParameter, taxCodeParameter, typeParameter);
        }
    
        public virtual ObjectResult<SyncCategory_QuyTrinhPhatHanh_Result> SyncCategory_QuyTrinhPhatHanh(string taxCode, string jobType)
        {
            var taxCodeParameter = taxCode != null ?
                new ObjectParameter("TaxCode", taxCode) :
                new ObjectParameter("TaxCode", typeof(string));
    
            var jobTypeParameter = jobType != null ?
                new ObjectParameter("JobType", jobType) :
                new ObjectParameter("JobType", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SyncCategory_QuyTrinhPhatHanh_Result>("SyncCategory_QuyTrinhPhatHanh", taxCodeParameter, jobTypeParameter);
        }
    }
}
