using eInvoice.Entity.EDM;
using eInvoice.Model;
using eInvoice.Model.DTOs.Invoice;
using eInvoice.Model.Invoice;
using eInvoice.Model.Invoice.Request;
using eInvoice.Model.Invoice.Response.searchInvoice;
using eInvoice.Repository.DataAccess;
using eInvoice.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eInvoice.Services.Service
{
    public class Invoice : IInvoice
    {
        /// <summary>
        /// service tìm kiếm hóa đơn
        /// </summary>
        /// <param name="searchInvoice"></param>
        /// <returns></returns>
        public searchInvoiceResponse searchInvoice(SearchInvoiceRequest searchInvoice)
        {
            SearchInvoiceDA da = new SearchInvoiceDA();
            searchInvoiceResponse response = new searchInvoiceResponse();
            List<PVOILInvoice> lstInvoice= da.selectListInvoice(searchInvoice);
            List<InvoicesModel> lstInvoiceModel = ModelBase.mapperStatic<PVOILInvoice, InvoicesModel>().Map<List<PVOILInvoice>, List<InvoicesModel>>(lstInvoice);
            foreach (InvoicesModel item in lstInvoiceModel)
            {
                searchInvoiceModel tmp = new searchInvoiceModel();
                tmp.key = item.Fkey.ToString ();
                tmp.invoice = item;
                tmp.invoice .products  = da.selectProductByInvoice(item.id);               
                response.invoices.Add(tmp);
            }
            return response;
        }
        /// <summary>
        /// service insert hóa đơn
        /// </summary>
        /// <param name="createInvoiceModel"></param>
        /// <returns></returns>
        public bool createInvoice(CreateInvoiceRequest createInvoiceModel)
        {
            CRUDInvoices cRUD = new CRUDInvoices();
            PVOILInvoice invoice = ModelBase.mapperStatic<InvoicesModel , PVOILInvoice>().Map< InvoicesModel, PVOILInvoice > (createInvoiceModel.invoice);

            cRUD.insertInvoiceProduct(invoice, createInvoiceModel.invoice .products);
            return true;
        }
    }
}
