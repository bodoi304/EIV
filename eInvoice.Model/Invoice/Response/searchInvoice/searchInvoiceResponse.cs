using eInvoice.Entity.EDM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eInvoice.Model.Invoice.Response.searchInvoice
{
    public class searchInvoiceResponse
    {
        public List<ErrorModel> error;
        public List<searchInvoiceModel> invoices;
        public searchInvoiceResponse()
        {
            this.invoices = new List<searchInvoiceModel>();
        }
    }
}
