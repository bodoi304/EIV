using eInvoice.Entity.EDM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eInvoice.Repository.DataAccess
{
    public class PublishInvoiceDA : DataAccessBase 
    {
        public PublishInvoice checkExistByID(String invSerial, String invPattern, int comID)
        {
            return dbInvoice.GetOne<PublishInvoice>(b => b.InvSerial == invSerial && b.InvPattern == invPattern
            && b.ComId == comID);
        }
    }
}
