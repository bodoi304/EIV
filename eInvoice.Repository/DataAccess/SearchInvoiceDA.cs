using eInvoice.Entity.EDM;
using eInvoice.Untilities.Common;
using eInvoice.Untilities.EFUntility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace eInvoice.Repository.DataAccess
{
    public class SearchInvoiceDA : DataAccessBase
    {
        /// <summary>
        /// Select invoice cho API api/pvoilbusiness/searchInvoice
        /// </summary>
        /// <param name="BranchCode"></param>
        /// <param name="CusTaxCode"></param>
        /// <param name="ComTaxCode"></param>
        /// <param name="username"></param>
        /// <returns></returns>
        public List<PVOILInvoice> selectListInvoice(String maDiemxuatHD, String username, String taxCode, String buyerTaxCode,
            DateTime from, DateTime to)
        {
            var predicate = PredicateBuilder.True<PVOILInvoice>();
            if (!string.IsNullOrEmpty(maDiemxuatHD))
            {
                predicate = predicate.And(t => t.BranchCode == maDiemxuatHD);
            }
            if (!string.IsNullOrEmpty(username))
            {
                predicate = predicate.And(t => t.CreateBy == username);
            }
            if (!string.IsNullOrEmpty(taxCode))
            {
                predicate = predicate.And(t => t.ComTaxCode == taxCode);
            }
            if (!string.IsNullOrEmpty(buyerTaxCode))
            {
                predicate = predicate.And(t => t.CusTaxCode == buyerTaxCode);
            }
            if (!(from == DateTime.MinValue))
            {
                predicate = predicate.And(t => t.ArisingDate >= from);
            }
            if (!(to == DateTime.MinValue))
            {
                predicate = predicate.And(t => t.ArisingDate <= to);
            }

            predicate = predicate.And(t => t.Status == (int)Constants.InvoiceStatus.Phat_Hanh);

            return dbInvoice.Filter<PVOILInvoice>(predicate).ToList();
        }

        public PVOILInvoice selectItemInvoiceByFKey(String Fkey)
        {

            return dbInvoice.GetOne<PVOILInvoice>(b => b.Fkey == Fkey);
        }
        /// <summary>
        /// Select product by invoce
        /// </summary>
        /// <param name="invID"></param>
        /// <returns></returns>
        public List<ProductInv> selectProductByInvoice(int invID)
        {
            return dbInvoice.Filter<ProductInv>(b => b.InvID == invID).ToList();
        }

        public InvTemplate_GetTemplateInvoice_Result InvTemplate_GetTemplateInvoice(String pattern, String taxCode)
        {
            InvTemplate_GetTemplateInvoice_Result obj = dbInvoice.db.InvTemplate_GetTemplateInvoice(pattern, taxCode).FirstOrDefault<InvTemplate_GetTemplateInvoice_Result>();
            return obj;
        }
    }
}
