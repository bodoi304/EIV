using eInvoice.Entity.EDM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eInvoice.Repository.DataAccess
{
    public class WareHouseDA : DataAccessBase
    {
        public Warehouse checkExistWarehouse(int cOutputWarehouseID)
        {
            return dbInvoice.GetOne<Warehouse>(b => b.Id == cOutputWarehouseID);
        }
    }
}
