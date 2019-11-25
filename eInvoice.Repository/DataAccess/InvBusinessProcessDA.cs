using eInvoice.Entity.EDM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eInvoice.Repository.DataAccess
{
    public class InvBusinessProcessDA : DataAccessBase
    {
        public InvBusinessProcess checkExist(int idInv)
        {
            return dbInvoice.GetOne<InvBusinessProcess>(b => b.InvID == idInv);
        }
    }
}
