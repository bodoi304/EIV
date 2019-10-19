using eInvoice.Entity.EDM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eInvoice.Repository.DataAccess
{
   public class CustomerDA : DataAccessBase 
    {
        public Customer checkExistBuyer(String buyer)
        {
            return dbInvoice.GetOne<Customer>(b => b.Buyer  == buyer);
        }
    }
}
