using eInvoice.Entity.EDM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eInvoice.Repository.DataAccess
{
    public class BusinessDepartmentDA : DataAccessBase 
    {
        public BusinessDepartment checkExistByID(int businessDepartmentID)
        {
            return dbInvoice.GetOne<BusinessDepartment>(b => b.BusinessDepartmentID  == businessDepartmentID);
        }
    }
}
