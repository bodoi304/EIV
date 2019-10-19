using eInvoice.Model.DTOs.Invoice;
using eInvoice.Model.Invoice;
using eInvoice.Model.Invoice.Request;
using eInvoice.Model.Invoice.Response.createInvoice;
using eInvoice.Model.Invoice.Response.searchInvoice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eInvoice.Services.Interface
{
    public interface IInvoice
    {
        searchInvoiceResponse searchInvoice(SearchInvoiceRequest searchInvoice);

        createInvoiceResponse createInvoice(CreateInvoiceRequest createInvoiceModel);

        byte[] exportPDF(ExportInvoiceRequest exportObj);
    }
}
