using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
namespace eInvoice.Untilities.LogsModule
{
    public class Logs
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        //Ghi log lỗi và trả về Response kèm Status code
        public static HttpResponseException Error(Exception ex, HttpStatusCode statusCode,String error)
        {
            log.Error(ex.Message + "-" + ex.StackTrace);
            var response = new HttpResponseMessage(statusCode)
            {
                Content = new StringContent(error),
                ReasonPhrase = "Exception"

            };
            return new HttpResponseException(response);
        }
    }
}
