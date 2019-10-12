using eInvoice.API.Filter;
using eInvoice.Model;
using eInvoice.Model.Category.Response.syncCategory;
using eInvoice.Model.DTOs.Invoice;
using eInvoice.Model.Invoice;
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
using System.Web.Http.ModelBinding;

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
        /// API trả về danh mục: api/pvoilbusiness/syncCategory
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpPost]
        public List<SyncCategoryResponse> syncCategory(SyncCategoryRequest objRequest)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    return invoiceCategory.syncCategory(objRequest);
                }
                catch (Exception ex)
                {
                    throw Logs.ErrorException(ex, HttpStatusCode.BadRequest, ConfigMultiLanguage.getMess(ConstantsMultiLanguageKey.LOI_CHUNG));
                }

            }
            else
            {
                throw Logs.Error(HttpStatusCode.BadRequest, Untility.getError(ModelState).ToString());
            }
        }
        /// <summary>
        /// API kiểm tra kết quả xử lý hóa đơn dự thảo từ FAST: api/pvoilbusiness/searchInvoice
        /// </summary>
        /// <param name="objRequest"></param>
        /// <returns></returns>
        [HttpPost]
        public searchInvoiceResponse searchInvoice(SearchInvoiceRequest objRequest)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    return invoice.searchInvoice(objRequest);
                }
                else
                {
                    throw Logs.Error(HttpStatusCode.BadRequest, Untility.getError(ModelState).ToString());
                }
            }
            catch (Exception ex)
            {
                throw Logs.ErrorException(ex, HttpStatusCode.BadRequest, ConfigMultiLanguage.getMess(ConstantsMultiLanguageKey.LOI_CHUNG));
            }
        }

        [HttpPost]
        public bool createInvoice(CreateInvoiceRequest objRequest)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    return invoice.createInvoice(objRequest);
                }
                else
                {
                    throw Logs.Error(HttpStatusCode.BadRequest, Untility.getError(ModelState).ToString());
                }
            }
            catch (Exception ex)
            {
                throw Logs.ErrorException(ex, HttpStatusCode.BadRequest, ConfigMultiLanguage.getMess(ConstantsMultiLanguageKey.LOI_CHUNG));
            }
        }
    }
}
