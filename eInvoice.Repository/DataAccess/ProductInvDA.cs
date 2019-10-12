using eInvoice.Entity.EDM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eInvoice.Repository.DataAccess
{
    public class ProductInvDA : DataAccessBase
    {
        public void  insertProduct(List<ProductInv> lstProductObj)
        {
             dbInvoice.Insert<ProductInv>(lstProductObj);
           
        }
    }
}
