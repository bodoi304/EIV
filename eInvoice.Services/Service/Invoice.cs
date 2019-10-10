using eInvoice.Entity.EDM;
using eInvoice.Model.DTOs.Invoice;
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
        public searchInvoiceResponse searchInvoice(SearchInvoiceRequest searchInvoice)
        {
            SearchInvoiceDA da = new SearchInvoiceDA();
            searchInvoiceResponse response = new searchInvoiceResponse();
            List<PVOILInvoice> lstInvoice= da.selectListInvoice(searchInvoice);
            foreach (PVOILInvoice item in lstInvoice)
            {
                searchInvoiceModel tmp = new searchInvoiceModel();
                tmp.key = item.Fkey.ToString ();
                tmp.invoice = item;
                tmp.invoice .products  = da.selectProductByInvoice(item.id);               
                response.invoices.Add(tmp);
            }
            return response;
        }
    }
}
