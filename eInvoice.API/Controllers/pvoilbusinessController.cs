using eInvoice.API.Filter;
using eInvoice.Model;
using eInvoice.Model.Category.Response.syncCategory;
using eInvoice.Model.DTOs.Invoice;
using eInvoice.Model.Invoice;
using eInvoice.Model.Invoice.Request;
using eInvoice.Model.Invoice.Response.createInvoice;
using eInvoice.Model.Invoice.Response.searchInvoice;
using eInvoice.MultiLanguages;
using eInvoice.Services.Interface;
using eInvoice.Untilities.Common;
using eInvoice.Untilities.LogsModule;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
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
        /// {
        ///  "username":"huyhq",
        ///  "taxCode":"1900291730",
        ///  "CatType": "ALL"
        ///}

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
                    throw Logs.ErrorException(ex, HttpStatusCode.BadGateway, UtilitesModel.getError(ConstantsMultiLanguageKey.E_COMMON));
                }
            }
            else
            {
                throw Logs.Error(HttpStatusCode.BadRequest, UtilitesModel.getError(ModelState));
            }
        }


        /// <summary>
        /// API kiểm tra kết quả xử lý hóa đơn dự thảo từ FAST: api/pvoilbusiness/searchInvoice
        /// </summary>
        /// <param name="objRequest"></param>
        /// <returns></returns>
        /// {
        ///  "maDiemxuatHD": "",
        /// "username": "",
        ///  "taxCode": "10008081411",
        ///  "buyerTaxCode": "",
        ///  "from": "Apr 9, 2019 12:00:00 AM",
        ///  "to": "Mar 10, 2019 12:00:00 AM",
        /// "reSynInvoice": "true"
        [HttpPost]
        public object searchInvoice(SearchInvoiceRequest objRequest)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    return invoice.searchInvoice(objRequest);
                }
                else
                {
                    searchInvoiceResponse returnObj = new searchInvoiceResponse();
                    returnObj.error = UtilitesModel.getErrorList(ModelState);
                    return returnObj;
                }
            }
            catch (Exception ex)
            {
                throw Logs.ErrorException(ex, HttpStatusCode.BadRequest, ex.Message + " - " + ex.StackTrace);
            }

        }


        /// <summary>
        /// API them hóa đơn : api/pvoilbusiness/createInvoice
        /// </summary>
        /// <param name="objRequest"></param>
        /// <returns></returns>
        [HttpPost]
        public createInvoiceResponse createInvoice(CreateInvoiceRequest objRequest)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    return invoice.createInvoice(objRequest);
                }
                else
                {
                    createInvoiceResponse returnObj = new createInvoiceResponse();
                    returnObj.key = objRequest.key;
                    returnObj.taxCode = objRequest.invoice.ComTaxCode;
                    returnObj.result = false;
                    returnObj.error = UtilitesModel.getErrorList(ModelState);
                    return returnObj;
                }
            }
            catch (Exception ex)
            {
                throw Logs.ErrorException(ex, HttpStatusCode.BadRequest, ex.Message + " - " + ex.StackTrace);
            }
        }


        [HttpGet]
        public HttpResponseMessage exportPDF(ExportInvoiceRequest objRequest)
        {

            byte[] invoicePdf = invoice.exportPDF(objRequest);

            HttpResponseMessage result = null;
            result = Request.CreateResponse(HttpStatusCode.OK);
            result.Content = new ByteArrayContent(invoicePdf);
            result.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment");
            result.Content.Headers.ContentDisposition.FileName = "invoice_DuThao" + ".pdf";

            return result;

        }



    }
}
