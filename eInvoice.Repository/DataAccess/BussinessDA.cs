using eInvoice.Entity.EDM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eInvoice.Repository.DataAccess
{
    public class BussinessDA :DataAccessBase 
    {
        public Business checkExistByID(int businessID)
        {
            return dbInvoice.GetOne<Business>(b => b.BusinessID  == businessID);
        }
    }
}
