
using eInvoice.Repository.Interface;
using eInvoice.Services.Interface;
using eInvoice.Untilities.Common;
using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eInvoice.Repository.DataAccess;
using eInvoice.Entity.EDM;

namespace eInvoice.Services.Service
{
    public class AuthenticationService : IAuthentication
    {
       
        //Inject DB theo cấu hình qua Constructor
       
        public bool checkAuthentication(string itemAuthentication,  string IP,int timeOutLogin)
        {
            //khởi tao instance truy vấn kho dữ liệu
            ApiUserAccessDA dhApiUser = new ApiUserAccessDA();
            //decode base64 thông tin user
            String[] authentication = Untility.decodeBase64(itemAuthentication).Split(':');

            //tag authentication không đúng định dạng
            if (authentication.Length < 2)
            {
                return false;
            }
            String UserName = authentication[0];
            String Password = authentication[1];
            //Tạo memory cache để lưu thông tin request API
            MemoryCacher cacher = new MemoryCacher();
            //Tạo token
            String token = Untility.getHashValueOfString(UserName + Password + IP);
            //Kiểm tra token đã được đăng nhập chưa
            if (cacher.GetValue(token) != null)
            {
                return true;
            }
            else
            {
                //chưa được đăng nhập thì kiểm tra trong DB
                ApiUserAccess user = dhApiUser.checkExist(UserName, Password);
                if (user == null)
                {
                    return false;
                }
                else
                {
                    //Kiểm tra IP của user có được truy cập hay không
                    if (!String.IsNullOrEmpty (user.IpMachine) && !user.IpMachine.Contains (IP))
                    {
                        return false;
                    }
                    //Kiểm tra user có được active hay không
                    if (user.Status == Constants.StatusUserAPIAccess.NONACTIVE)
                    {
                        return false;
                    }
                    //Lưu token sau khi đã được login và thời gian timeout login
                    cacher.Add(token, UserName + "@" + Password + "@" + IP, DateTimeOffset.UtcNow.AddMinutes(timeOutLogin));
                    return true;
                }
            }
        }
    }
}
