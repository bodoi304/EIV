using eInvoice.API.Filter;
using eInvoice.MultiLanguages;
using eInvoice.Repository.EDM;
using eInvoice.Services.Interface;
using eInvoice.Untilities.LogsModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace eInvoice.API.Controllers
{
    [AuthenticationCustomer]
    public class pvoilbusinessController : ApiController
    {
      
        private IInvoiceCategorys invoiceCategory;
        //Inject service danh mục
        public pvoilbusinessController(IInvoiceCategorys invoiceCategory)
        {
            this.invoiceCategory = invoiceCategory;
        }
        // API phục vụ cho đồng bộ danh mục
        [HttpGet]   
        public List<Invoice03> syncCategory()
        {
            try
            {
                return invoiceCategory.syncCategory ();
            }
            catch (Exception ex)
            {
                throw Logs.Error(ex, HttpStatusCode.BadRequest, ConfigMultiLanguage.getMess(ConstantsKey.LOI_TIM_KIEM_HOA_DON));
            }        
        }   
    }
}
