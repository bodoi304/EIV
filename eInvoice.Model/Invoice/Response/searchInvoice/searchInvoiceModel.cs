using eInvoice.Entity.EDM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eInvoice.Model.Invoice.Response.searchInvoice
{
    public class searchInvoiceModel
    {
        public string key;
        public string Serial;
        public string Pattern;
        public int InvoiceNo;

        public int Soduthao;
        public int Trangthaikiemtra;
        public String Ghichu;
        public int? Type;
        public int? Status;
        public bool? Draftcancel;
        //public InvoicesModel invoice;

        //public searchInvoiceModel()
        //{
        //    this.invoice = new InvoicesModel();
        //}
    }
}
