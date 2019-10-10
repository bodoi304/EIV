using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace eInvoice.Model.Invoice.Request
{
    /// <summary>
    /// Đối tượng Request cho API: api/pvoilbusiness/syncCategory
    /// </summary>
    public class SyncCategoryRequest : ModelBase 
    {
        public string userName { get; set; }
        public string taxCode { get; set; }

        public override IMapper mapper()
        {
            throw new NotImplementedException();
        }
    }
}
