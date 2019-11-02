using eInvoice.Entity.EDM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eInvoice.Repository.DataAccess
{
    public class InvTemplateDA : DataAccessBase
    {
        public InvTemplate checkExistTypeView(int invoiceType, String invoiceview)
        {
            String StrinvoiceType = invoiceType.ToString();
            return dbInvoice.GetOne<InvTemplate>(b => b.InvoiceType.Equals(StrinvoiceType) && b.InvoiceView == invoiceview);
        }
    }
}
