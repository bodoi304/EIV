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
    public class InvoiceCategorys : IInvoiceCategorys
    {
        private IDBContextHelper db;
        //Inject DB theo cấu hình qua Constructor
        public InvoiceCategorys(IDBContextHelper db)
        {
            this.db = db;
        }
        //Service đồng bộ danh mục
        public List<Invoice03> syncCategory()
        {
           return db.Filter<Invoice03>(b => true, 1, 20, "id");
        }
    }
}
