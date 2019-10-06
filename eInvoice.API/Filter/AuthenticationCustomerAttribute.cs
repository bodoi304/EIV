using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace eInvoice.API.Filter
{
    public class AuthenticationCustomerAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            IEnumerable<String> authenHeader;
            //Kiểm tra header có tag Authentication không
            if (!actionContext.Request.Headers.TryGetValues("Authentication", out authenHeader))
            {
                actionContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
            }
            //validate Authentication
            foreach (String item in authenHeader)
            {
                String a = item;
     
           
            }
        
        }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {

        }
    }
}