using eInvoice.Repository.EDM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eInvoice.Services.Interface
{
    //các service danh mục
    public interface IInvoiceCategorys
    {
        //Service đồng bộ danh mục
        List<Invoice03> syncCategory();
    }
}
