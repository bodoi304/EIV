using eInvoice.Entity.EDM;
using Newtonsoft.Json;
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
        [JsonIgnore]
        public String Data { get; set; }

        public String BusinessDepartmentID { get; set; } 
        
    }
}
