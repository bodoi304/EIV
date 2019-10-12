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
        [JsonProperty("catName")]
        public String catName { get; set; }
        public List<Object>  data { get; set; }
    }
}
