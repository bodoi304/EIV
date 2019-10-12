using eInvoice.Entity.EDM;
using eInvoice.Model.Invoice;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eInvoice.Repository.DataAccess
{
    public class CRUDInvoices : DataAccessBase
    {
        public int  insertInvoices(PVOILInvoice invoiceObj)
        {
            dbInvoice.Insert<PVOILInvoice>(invoiceObj);
            return invoiceObj.id;
        }



        public bool insertInvoiceProduct(PVOILInvoice invoiceObj,List <ProductInv> lstProductObj)
        {
            ProductInvDA cPro = new ProductInvDA();
            using (DbContextTransaction transaction = dbInvoice.db.Database .BeginTransaction())
            {
                try
                {
                    int idInvoice= insertInvoices(invoiceObj);
                    foreach (ProductInv item in lstProductObj)
                    {
                        item.id = Guid.NewGuid(); 
                        item.InvID = idInvoice;
                    }
                    cPro.insertProduct(lstProductObj);
                    transaction.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return false;
                }
            }
        }
    }
}
