using AutoMapper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eInvoice.Model.Category.Response.syncCategory
{
    public class SyncCategoryResponse 
    {
        [JsonProperty("DIEMXUAT")]
        public DMDiemXuatModel DiemXuat { get; set; }
        [JsonProperty("KHO")]
        public DMKhoModel  Kho { get; set; }
        [JsonProperty("MAKETOAN")]
        public DMMaKeToanModel MaKeToan { get; set; }
        [JsonProperty("NGHIEPVU")]
        public DmNghiepVuModel NghiepVu { get; set; }
        public string result { get; set; }
        public string error { get; set; }

        public SyncCategoryResponse()
        {
            DiemXuat = new DMDiemXuatModel();
            Kho = new DMKhoModel();
            MaKeToan = new DMMaKeToanModel();
            NghiepVu = new DmNghiepVuModel();
        }

    }
}
