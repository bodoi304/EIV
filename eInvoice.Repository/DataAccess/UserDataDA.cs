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
        public userdata checkExist(String userName )
        {
            return dbInvoice.GetOne<userdata>(b => b.username == userName  );
        }

        public userdata checkExistByUserID(int userID)
        {
            return dbInvoice.GetOne<userdata>(b => b.userid  == userID );
        }

        public userdata_CheckUserAPI_Result checkExistByUserNameAndPassword(String userName,String taxCode, int Type)
        {
            return dbInvoice.db.userdata_CheckUserAPI(userName, taxCode, Type).FirstOrDefault();
           
        }
    }
}
