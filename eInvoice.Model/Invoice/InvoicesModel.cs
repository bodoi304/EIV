using eInvoice.Entity.EDM;
using eInvoice.Untilities.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eInvoice.Model.Invoice
{
    public class InvoicesModel : PVOILInvoice
    {
        public List<ProductInv> products { get; set; }
        [JsonIgnore]
        public String Data { get; set; }

        public int BusinessDepartmentID { get; set; }

        [JsonConverter(typeof(CustomFormatDateTimeConverter))]
        public Nullable<System.DateTime> CreateDate { get; set; }
        [JsonConverter(typeof(CustomFormatDateTimeConverter))]
        public Nullable<System.DateTime> PublishDate { get; set; }
        [JsonConverter(typeof(CustomFormatDateTimeConverter))]
        public Nullable<System.DateTime> ArisingDate { get; set; }
        [JsonConverter(typeof(CustomFormatDateTimeConverter))]
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        [JsonConverter(typeof(CustomFormatDateTimeConverter))]
        public Nullable<System.DateTime> CreatedDate { get; set; }
        [JsonConverter(typeof(CustomFormatDateTimeConverter))]
        public Nullable<System.DateTime> CheckedDate1 { get; set; }
        [JsonConverter(typeof(CustomFormatDateTimeConverter))]
        public Nullable<System.DateTime> CheckedDate2 { get; set; }
        [JsonConverter(typeof(CustomFormatDateTimeConverter))]
        public Nullable<System.DateTime> CheckedDate3 { get; set; }
        [JsonConverter(typeof(CustomFormatDateTimeConverter))]
        public Nullable<System.DateTime> SignedDate { get; set; }
        [JsonConverter(typeof(CustomFormatDateTimeConverter))]
        public Nullable<System.DateTime> LastestUpdateDate { get; set; }
        [JsonConverter(typeof(CustomFormatDateTimeConverter))]
        public Nullable<System.DateTime> CheckedDate4 { get; set; }
        [JsonConverter(typeof(CustomFormatDateTimeConverter))]
        public Nullable<System.DateTime> InvCommanDate { get; set; }
        [JsonConverter(typeof(CustomFormatDateTimeConverter))]
        public Nullable<System.DateTime> UpdateDate { get; set; }
        [JsonConverter(typeof(CustomFormatDateTimeConverter))]
        public Nullable<System.DateTime> LetterDate { get; set; }
        [JsonConverter(typeof(CustomFormatDateTimeConverter))]
        public Nullable<System.DateTime> VehiclePersonDate { get; set; }
        [JsonConverter(typeof(CustomFormatDateTimeConverter))]
        public Nullable<System.DateTime> ExportDate { get; set; }
        [JsonConverter(typeof(CustomFormatDateTimeConverter))]
        public Nullable<System.DateTime> ReceiptDate { get; set; }
        [JsonConverter(typeof(CustomFormatDateTimeConverter))]
        public Nullable<System.DateTime> ReceiptPersonDate { get; set; }
        [JsonConverter(typeof(CustomFormatDateTimeConverter))]
        public Nullable<System.DateTime> ExpireDate { get; set; }
        [JsonConverter(typeof(CustomFormatDateTimeConverter))]
        public Nullable<System.DateTime> InvoiceDate { get; set; }
        
    }
}
