using eInvoice.Repository.DataContext;
using eInvoice.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eInvoice.Repository
{
    public class DataAccessBase
    {
        //Khởi tạo đối tượng DB context theo singleton
        public static readonly IDBContextInvoiceSQL dbInvoice= new DBContextInvoiceSQL();
  
    }
}
