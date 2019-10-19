using eInvoice.Entity.EDM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eInvoice.Repository.DataAccess
{
  public class UserDataDA : DataAccessBase
    {
        public userdata checkExist(String userName , int type)
        {
            return dbInvoice.GetOne<userdata>(b => b.username == userName && b.Type ==type );
        }
    }
}
