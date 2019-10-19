using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eInvoice.Model.Invoice.Response.createInvoice
{
    public class createInvoiceResponse
    {
        public string key;
        public string taxCode;
        public Boolean result;
        public List<ErrorModel> error;
    }
}
