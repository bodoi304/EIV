using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using eInvoice.Entity.EDM;

namespace eInvoice.Model.Category.Response.syncCategory
{
    public class DiemXuatModel : ModelBase 
    {
        public int id { get; set; }
        public string MaDV { get; set; }
        public string Ten { get; set; }
        public string Ma { get; set; }
        public string DiaChi { get; set; }

        public override IMapper mapper()
        {
            var configuration = new MapperConfiguration(cfg => { cfg.CreateMap<SyncCategory_DiemXuat_Result, DiemXuatModel>(); });
            return configuration.CreateMapper();
        }
    }
}
