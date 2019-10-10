using eInvoice.Entity.EDM;
using eInvoice.Model.DTOs.Invoice;
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
        public List<PVOILInvoice> selectListInvoice(SearchInvoiceRequest searchInvoice)
        {
            var predicate = PredicateBuilder.True<PVOILInvoice>();
            if (!string.IsNullOrEmpty(searchInvoice.maDiemxuatHD))
            {
                predicate = predicate.And(t => t.BranchCode == searchInvoice.maDiemxuatHD);
            }
            if (!string.IsNullOrEmpty(searchInvoice.username))
            {
                predicate = predicate.And(t => t.CreateBy == searchInvoice.username);
            }
            if (!string.IsNullOrEmpty(searchInvoice.taxCode))
            {
                predicate = predicate.And(t => t.ComTaxCode == searchInvoice.taxCode);
            }
            if (!string.IsNullOrEmpty(searchInvoice.buyerTaxCode))
            {
                predicate = predicate.And(t => t.CusTaxCode == searchInvoice.buyerTaxCode);
            }
            if (!(searchInvoice.from == DateTime .MinValue ))
            {
                predicate = predicate.And(t => t.ArisingDate >= searchInvoice.from);
            }
            if (!(searchInvoice.to == DateTime.MinValue))
            {
                predicate = predicate.And(t => t.ArisingDate <= searchInvoice.to);
            }
            return dbInvoice.Filter<PVOILInvoice>(predicate).ToList ();
        }
        /// <summary>
        /// Select product by invoce
        /// </summary>
        /// <param name="invID"></param>
        /// <returns></returns>
        public List<ProductInv> selectProductByInvoice(int invID)
        {
            return dbInvoice.Filter<ProductInv>(b => b.InvID  == invID).ToList();
        }

        public Expression<Func<PVOILInvoice, bool>> predecateString(String[] buildWhere,Object[] objectWhere)
        {
            var predicate = PredicateBuilder.True<PVOILInvoice>();
          
            for (int i = 0; i < buildWhere.Length; i++)
            {
                if (objectWhere[i].GetType().Equals(typeof(String)))
                {
                    String objTmp = (String)objectWhere[i];
                    if (!string.IsNullOrEmpty(objTmp))
                    {
                        predicate = predicate.And(t => t.BranchCode == objTmp);
                    }
                }
            }

            return predicate;
           
        }

        private object GetValue(MemberExpression member)
        {
            var objectMember = Expression.Convert(member, typeof(object));

            var getterLambda = Expression.Lambda<Func<object>>(objectMember);

            var getter = getterLambda.Compile();

            return getter();
        }
    }
}
