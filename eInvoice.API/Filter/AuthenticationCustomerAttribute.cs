using eInvoice.Services.Interface;
using eInvoice.Untilities;
using eInvoice.Untilities.Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace eInvoice.API.Filter
{
    public class AuthenticationCustomerAttribute : ActionFilterAttribute
    {
        //Thời gian timeout login request
        public static int timeOutLogin =Convert .ToInt32 ( ConfigurationManager.AppSettings["TimeOutLogin"]);

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            //Inject service không qua contructor
            IAuthentication auth = GlobalConfiguration.Configuration.DependencyResolver
           .GetService(typeof(IAuthentication)) as IAuthentication;
            IEnumerable<String> authenHeader;
            //Kiểm tra header có tag Authentication không
            if (!actionContext.Request.Headers.TryGetValues("Authentication", out authenHeader))
            {
                actionContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
            }
            if (authenHeader != null)
            {
                foreach (String item in authenHeader)
                {
                    //check Authentication
                    Boolean isLogin = auth.checkAuthentication(item, actionContext.Request.GetClientIpAddress(), timeOutLogin);
                    //login không thành công
                    if (!isLogin)
                    {
                        actionContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
                    }
                    break;
                }
            }
          

        }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {

        }
    }
}