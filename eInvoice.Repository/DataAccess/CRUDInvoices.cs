using eInvoice.Entity.EDM;
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
        /// <summary>
        /// them invoice
        /// </summary>
        /// <param name="invoiceObj"></param>
        /// <returns></returns>
        public int  insertInvoices(PVOILInvoice invoiceObj)
        {
            dbInvoice.Insert<PVOILInvoice>(invoiceObj);
            return invoiceObj.id;
        }


        /// <summary>
        /// Thêm invoice kèm product
        /// </summary>
        /// <param name="invoiceObj"></param>
        /// <param name="lstProductObj"></param>
        /// <returns></returns>
        public bool insertInvoiceProduct(PVOILInvoice invoiceObj,List <ProductInv> lstProductObj)
        {
            ProductInvDA cPro = new ProductInvDA();
            using (DbContextTransaction transaction = dbInvoice.db.Database.BeginTransaction())
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
                    throw  ex;
                }
            }
        }
    }
}
