using eInvoice.Entity.EDM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eInvoice.Model.Invoice
{
    public class InvoicesModel : PVOILInvoice
    {
        public List<ProductInv> products { get; set; }
    }
}
