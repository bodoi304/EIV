using eInvoice.Repository.EDM;
using eInvoice.Repository.Interface;
using eInvoice.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eInvoice.Services.Service
{
    //impliment service danh mục
    public class InvoiceCategorysService : IInvoiceCategorys
    {
        //Service đồng bộ danh mục
        public List<Invoice03> syncCategory()
        {
            return new List<Invoice03>();
        }
    }
}
