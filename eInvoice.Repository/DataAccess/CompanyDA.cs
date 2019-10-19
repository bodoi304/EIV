using eInvoice.Entity.EDM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eInvoice.Repository.DataAccess
{
    public class CompanyDA : DataAccessBase
    {
        public Company checkExistTaxCode(String taxCode)
        {
            return dbInvoice.GetOne<Company>(b => b.TaxCode == taxCode);
        }

        public Company checkExistByID(int ID)
        {
            return dbInvoice.GetOne<Company>(b => b.id == ID);
        }
    }
}
