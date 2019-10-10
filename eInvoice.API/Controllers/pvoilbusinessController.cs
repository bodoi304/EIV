using eInvoice.API.Filter;
using eInvoice.Model;
using eInvoice.Model.Category.Response.syncCategory;
using eInvoice.Model.DTOs.Invoice;
using eInvoice.Model.Invoice.Request;
using eInvoice.Model.Invoice.Response.searchInvoice;
using eInvoice.MultiLanguages;
using eInvoice.Services.Interface;
using eInvoice.Untilities.Common;
using eInvoice.Untilities.LogsModule;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
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
        private IInvoice invoice;
        //Inject service danh mục
        public pvoilbusinessController(IInvoiceCategorys invoiceCategory, IInvoice invoice)
        {
            this.invoiceCategory = invoiceCategory;
            this.invoice = invoice;
        }
        /// <summary>
        /// API kiểm tra kết quả xử lý hóa đơn dự thảo từ FAST
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpPost]
        public SyncCategoryResponse syncCategory(SyncCategoryRequest objRequest)
        {

            String validateRequest = ModelBase.validateRequiredObject(objRequest, new string[] { "userName", "taxCode" });
            if (!String.IsNullOrEmpty(validateRequest))
            {
                throw Logs.Error(HttpStatusCode.BadRequest, validateRequest);
            }
            try
            {
                return invoiceCategory.syncCategory(objRequest);
            }
            catch (Exception ex)
            {
                throw Logs.ErrorException(ex, HttpStatusCode.BadRequest, ConfigMultiLanguage.getMess(ConstantsMultiLanguageKey.LOI_CHUNG));
            }
        }
      
        [HttpPost]
        public searchInvoiceResponse searchInvoice(SearchInvoiceRequest objRequest)
        {

            String validateRequest = ModelBase.validateRequiredObject(objRequest, new string[] { "from", "to", "taxCode" });
            if (!String.IsNullOrEmpty(validateRequest))
            {
                throw Logs.Error(HttpStatusCode.BadRequest, validateRequest);
            }
            try
            {
                return invoice.searchInvoice (objRequest);
            }
            catch (Exception ex)
            {
                throw Logs.ErrorException(ex, HttpStatusCode.BadRequest, ConfigMultiLanguage.getMess(ConstantsMultiLanguageKey.LOI_CHUNG));
            }
        }
    }
}
