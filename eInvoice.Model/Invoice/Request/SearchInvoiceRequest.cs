using AutoMapper;
using eInvoice.Untilities.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static eInvoice.Untilities.Common.Constants;

namespace eInvoice.Model.DTOs.Invoice
{
    /// <summary>
    /// Đối tượng Request cho API: api/pvoilbusiness/searchInvoice
    /// </summary>
    public class SearchInvoiceRequest : ModelBase,IValidatableObject
    {

        public string maDiemxuatHD { get; set; }
        public string username { get; set; }
        public string taxCode { get; set; }
        public string buyerTaxCode { get; set; }
        [JsonConverter(typeof(CustomDateTimeConverter))]
        public DateTime from { get; set; }
        [JsonConverter(typeof(CustomDateTimeConverter))]
        public DateTime to { get; set; }
        public Boolean reSynInvoice { get; set; }

        public override IMapper mapper()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// ham validate model
        /// </summary>
        /// <param name="validationContext"></param>
        /// <returns></returns>
        public IEnumerable<ValidationResult> Validate(System.ComponentModel.DataAnnotations.ValidationContext validationContext)
        {
            List<String> validateRequest = ModelBase.validateRequiredObject(new string[] { "from","to", "taxCode" , "username" }, new object[] { from, to, taxCode,username  });
            foreach (String item in validateRequest)
            {
                yield return new ValidationResult(item);
            }
            yield return ModelValidate.checkDateTuQuaNgayHienTai(from);

            yield return ModelValidate.checkDateDenQuaNgayHienTai(to);

            yield return ModelValidate.checkDateTuDenVuotQuaXNgay(DateTimeVuotQua.NGAY_7, from,to);

            yield return ModelValidate.checkDateDenLonHonTu(from,to);


        }
    }
}
