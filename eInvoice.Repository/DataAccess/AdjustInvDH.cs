using eInvoice.Entity.EDM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eInvoice.Repository.DataAccess
{
   public class AdjustInvDH : DataAccessBase
    {
        public void insertAdjustInv(AdjustInv adjustInv)
        {
            dbInvoice.Insert<AdjustInv>(adjustInv);

        }

        public AdjustInv CheckExists(Int64 invId)
        {
          return  dbInvoice.GetOne<AdjustInv>(b => b.InvId == invId);

        }

        public AdjustInv CheckExistsAdj(Int64 adjustInvId)
        {
            return dbInvoice.GetOne<AdjustInv>(b => b.AdjustInvId == adjustInvId);

        }
    }
}
