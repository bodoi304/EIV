using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eInvoice.Model.Category.Response.syncCategory
{
    public class DMDiemXuatModel 
    {
        public string catName { get; set; }
        public List<DiemXuatModel> data { get; set; }
    }
}
