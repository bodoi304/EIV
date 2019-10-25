using eInvoice.Entity.EDM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eInvoice.Repository.DataAccess
{
    public class CurrencyDA :DataAccessBase
    {
        public Currency checkExist(String code)
        {
            return dbInvoice.GetOne<Currency>(b => b.Code.ToUpper()  == code.ToUpper());
        }
    }
}
