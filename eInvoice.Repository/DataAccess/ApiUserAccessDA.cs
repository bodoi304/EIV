using eInvoice.Entity.EDM;
using eInvoice.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eInvoice.Repository.DataAccess
{
    public class ApiUserAccessDA : DataAccessBase
    {
        /// <summary>
        /// Check tồn tại user login
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public ApiUserAccess checkExist(String userName , String password)
        {
          return  dbInvoice.GetOne<ApiUserAccess>(b => b.UserName == userName && b.Password == password);
        }
    }
}
