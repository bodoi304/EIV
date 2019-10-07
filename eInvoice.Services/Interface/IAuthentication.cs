using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eInvoice.Services.Interface
{
    public interface  IAuthentication
    {
        /// <summary>
        /// Kiểm tra tài khoản request API
        /// </summary>
        /// <param name="itemAuthentication">giá trị Tag Authentication ở Header request</param>
        /// <param name="IP">địa chỉ IP máy request API</param>
        /// <param name="timeOutLogin">thời gian timeout login tính bằng phút</param>
        /// <returns>true: xác thực thành công - false: xác thực thất bại</returns>
        Boolean checkAuthentication(string itemAuthentication ,String IP,int timeOutLogin);
    }
}
