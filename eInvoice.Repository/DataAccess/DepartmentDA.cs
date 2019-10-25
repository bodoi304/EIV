using eInvoice.Entity.EDM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eInvoice.Repository.DataAccess
{
   public class DepartmentDA : DataAccessBase
    {
        public Department checkExistByID(int departmentID)
        {
            return dbInvoice.GetOne<Department>(b => b.id == departmentID);
        }
    }
}
