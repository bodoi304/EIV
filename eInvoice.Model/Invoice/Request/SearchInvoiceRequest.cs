using AutoMapper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eInvoice.Model.DTOs.Invoice
{
    /// <summary>
    /// Đối tượng Request cho API: api/pvoilbusiness/searchInvoice
    /// </summary>
    public class SearchInvoiceRequest : ModelBase
    {

        public string maDiemxuatHD { get; set; }
        public string username { get; set; }
        public string taxCode { get; set; }
        public string buyerTaxCode { get; set; }
        public DateTime from { get; set; }
        public DateTime to { get; set; }
        public Boolean reSynInvoice { get; set; }

        public override IMapper mapper()
        {
            throw new NotImplementedException();
        }
    }
}
