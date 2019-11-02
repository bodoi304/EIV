using eInvoice.Entity.EDM;
using eInvoice.Untilities.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace eInvoice.Model.Invoice
{

    public class ProductModel : ProductInv
    {
        [JsonIgnore]
        public String Data { get; set; }
        [JsonConverter(typeof(CustomFormatBooleanConverter))]
        public Nullable<bool> IsSum { get; set; }
    }

}