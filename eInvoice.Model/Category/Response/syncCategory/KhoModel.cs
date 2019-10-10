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
        public string MaKho { get; set; }
        public string TenKho { get; set; }
        public string DiaChi { get; set; }
        public string MaDV { get; set; }

        public override IMapper mapper()
        {
            var configuration = new MapperConfiguration(cfg => { cfg.CreateMap<Warehouse_SelectByTaxCode_Result, KhoModel>(); });
            return configuration.CreateMapper();
        }
    }
}
