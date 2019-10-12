using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using eInvoice.Entity.EDM;


namespace eInvoice.Model.Category.Response.syncCategory
{
    public class KhoModel : ModelBase 
    {
        public int id { get; set; }
        public string makho { get; set; }
        public string tenkho { get; set; }
        public string diachi { get; set; }
        public string donvi { get; set; }

        public override IMapper mapper()
        {
            var configuration = new MapperConfiguration(cfg => { cfg.CreateMap<Warehouse_SelectByTaxCode_Result, KhoModel>(); });
            return configuration.CreateMapper();
        }
    }
}
