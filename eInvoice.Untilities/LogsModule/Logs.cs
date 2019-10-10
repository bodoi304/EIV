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

        //
        /// <summary>
        /// Ghi log lỗi và trả về Response kèm Status code khi có exception
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="statusCode"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public static HttpResponseException ErrorException(Exception ex, HttpStatusCode statusCode,String error)
        {
            log.Error(ex.Message + "-" + ex.StackTrace);
            var response = new HttpResponseMessage(statusCode)
            {
                Content = new StringContent(error),
                ReasonPhrase = "Exception"

            };
            return new HttpResponseException(response);
        }

        /// <summary>
        /// Ghi log lỗi và trả về Response kèm Status code với những lỗi throw
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="statusCode"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public static HttpResponseException Error( HttpStatusCode statusCode, String error)
        {
            log.Error(error);
            var response = new HttpResponseMessage(statusCode)
            {
                Content = new StringContent(error),
                ReasonPhrase = "Exception"

            };
            return new HttpResponseException(response);
        }
    }
}
