using eInvoice.Entity.EDM;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static eInvoice.Untilities.Common.Constants;

namespace eInvoice.Repository.DataAccess
{
    public class CRUDInvoices : DataAccessBase
    {
        /// <summary>
        /// them invoice
        /// </summary>
        /// <param name="invoiceObj"></param>
        /// <returns></returns>
        public int insertInvoices(PVOILInvoice invoiceObj)
        {
            dbInvoice.Insert<PVOILInvoice>(invoiceObj);
            return invoiceObj.id;
        }

        public int updateInvoices(PVOILInvoice invoiceObj)
        {
            dbInvoice.Update<PVOILInvoice>(invoiceObj);
            return invoiceObj.id;
        }

        /// <summary>
        /// Thêm invoice kèm product
        /// </summary>
        /// <param name="invoiceObj"></param>
        /// <param name="lstProductObj"></param>
        /// <returns></returns>
        public int insertInvoiceProduct(PVOILInvoice invoiceObj, List<ProductInv> lstProductObj, String originKey)
        {
            ProductInvDA cPro = new ProductInvDA();
            PVOILInvoiceDA ctlPvoil = new PVOILInvoiceDA();
            AdjustInvDH cAdj = new AdjustInvDH();
            AdjustInv objAdj = new AdjustInv();
            int idInvoice = 0;
            Boolean draftCancel = invoiceObj.DraftCancel ?? false;
            PVOILInvoice objInv = ctlPvoil.checkExistInvoice(originKey, invoiceObj.ComTaxCode);
            PVOILInvoice objAdjInv = ctlPvoil.checkExistInvoice(invoiceObj.Fkey, invoiceObj.ComTaxCode);  
            int adjustInvId = objAdjInv != null ? objAdjInv.id : 0;
            //Set status cho table AdjustInv 
            if (draftCancel)
            {
                if (invoiceObj.Type == InvoiceType.Nomal || invoiceObj.Type == InvoiceType.ForReplace || invoiceObj.Type == InvoiceType.ForAdjustAccrete
          || invoiceObj.Type == InvoiceType.ForAdjustReduce
          || invoiceObj.Type == InvoiceType.ForAdjustInfo)
                {
                    objAdj.Status = StatusAdj.Du_Thao_Huy ;
                }
            }
            else
            {
                if (invoiceObj.Type == InvoiceType.ForReplace)
                {
                    objAdj.Status = StatusAdj.Du_Thao_Thay_The ;
                }

                else if (invoiceObj.Type == InvoiceType.ForAdjustAccrete
          || invoiceObj.Type == InvoiceType.ForAdjustReduce
          || invoiceObj.Type == InvoiceType.ForAdjustInfo)
                {
                    objAdj.Status = StatusAdj.Du_Thao_Dieu_Chinh ;
                }
                else
                {
                    objAdj.Status = 0;
                }
            }

            using (DbContextTransaction transaction = dbInvoice.db.Database.BeginTransaction())
            {
                try
                {
                    if (draftCancel && ((invoiceObj.Type == InvoiceType.Nomal ||
          invoiceObj.Type == InvoiceType.ForReplace || invoiceObj.Type == InvoiceType.ForAdjustAccrete
          || invoiceObj.Type == InvoiceType.ForAdjustReduce
          || invoiceObj.Type == InvoiceType.ForAdjustInfo)))
                    {

                        objAdjInv.Type = invoiceObj.Type;
                        objAdjInv.Status = invoiceObj.Status;
                        objAdjInv.DraftCancel = invoiceObj.DraftCancel;
                        updateInvoices(objAdjInv);
                        objAdj.InvId = objInv.id;
                        objAdj.AdjustInvId = adjustInvId;
                        objAdj.Description = invoiceObj.Name;
                        objAdj.Pattern = invoiceObj.Pattern;
                        objAdj.ProcessDate = DateTime.Now;
                        objAdj.ComID = invoiceObj.ComID;

                    }
                    else
                    {
                        idInvoice = insertInvoices(invoiceObj);
                        adjustInvId = objAdjInv != null ? objAdjInv.id : idInvoice;
                        int invID = objInv != null ? objInv.id : 0;
                        objAdj.InvId = invID;
                        objAdj.AdjustInvId = adjustInvId;
                        objAdj.Description = invoiceObj.Name;
                        objAdj.Pattern = invoiceObj.Pattern;
                        objAdj.ProcessDate = DateTime.Now;
                        objAdj.ComID = invoiceObj.ComID;

                        foreach (ProductInv item in lstProductObj)
                        {
                            item.id = Guid.NewGuid();
                            item.InvID = idInvoice;
                        }
                        cPro.insertProduct(lstProductObj);
                    }
                    //check insert table AdjustInv type=0 and status=0 and daftcancel=0  thì ko insert
                    if (invoiceObj.Type == InvoiceType.Nomal)
                    {
                        if (draftCancel)
                        {
                            cAdj.insertAdjustInv(objAdj);
                        }
                    }
                    else
                    {
                        cAdj.insertAdjustInv(objAdj);
                    }

                    transaction.Commit();
                    return idInvoice;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
        }
    }
}
