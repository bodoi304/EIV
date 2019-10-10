using AutoMapper;
using eInvoice.Entity.EDM;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eInvoice.Model.Category.Response.syncCategory
{
    public class MaKeToanModel : ModelBase
    {
        [JsonProperty("Ma")]
        public string AccountingAccCode{ get; set; }
        [JsonProperty("MoTa")]
        public string Description   { get; set; }

        public override IMapper mapper()
        {
            var configuration = new MapperConfiguration(cfg => { cfg.CreateMap<AccountingAccount, MaKeToanModel>(); });
            return configuration.CreateMapper();
        }
    }
}
