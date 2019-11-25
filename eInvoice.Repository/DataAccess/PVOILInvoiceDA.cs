using eInvoice.Entity.EDM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eInvoice.Repository.DataAccess
{
    public class PVOILInvoiceDA : DataAccessBase
    {
        public PVOILInvoice checkExistInvoice(String fkey)
        {

            return dbInvoice.GetOne<PVOILInvoice>(b => b.Fkey == fkey );
        }

        public PVOILInvoice checkExistInvoice(String fkey,String taxCode)
        {

            return dbInvoice.GetOne<PVOILInvoice>(b => b.Fkey == fkey && b.ComTaxCode== taxCode);
        }
    }
}
