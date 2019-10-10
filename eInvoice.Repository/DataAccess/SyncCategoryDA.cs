using eInvoice.Entity.EDM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eInvoice.Repository.DataAccess
{
   public class SyncCategoryDA : DataAccessBase
    {
        public List<Warehouse_SelectByTaxCode_Result> Warehouse_SelectByTaxCode(String taxCode)
        {
            List<Warehouse_SelectByTaxCode_Result> lst= dbInvoice.db.Warehouse_SelectByTaxCode(taxCode).ToList<Warehouse_SelectByTaxCode_Result>();
            return lst;
        }

        public List<SyncCategory_DiemXuat_Result> SyncCategory_DiemXuat(String userName, String taxCode)
        {
            List<SyncCategory_DiemXuat_Result> lst = dbInvoice.db.SyncCategory_DiemXuat(userName,taxCode).ToList<SyncCategory_DiemXuat_Result>();
            return lst;
        }

        public List<SyncCategory_NghiepVu_Result> SyncCategory_NghiepVu(String userName, String taxCode)
        {
            List<SyncCategory_NghiepVu_Result> lst = dbInvoice.db.SyncCategory_NghiepVu (userName,taxCode).ToList<SyncCategory_NghiepVu_Result>();
            return lst;
        }

        public List<AccountingAccount> AccountingAccount_SelectAll()
        {
          return   dbInvoice.GetAll<AccountingAccount>().ToList();
        }
    }
}
