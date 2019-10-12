using AutoMapper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public DateTime from { get; set; }
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
            String validateRequest = ModelBase.validateRequiredObject(new string[] { "from","to", "taxCode" }, new object[] { from, to, taxCode });
            if (!String.IsNullOrEmpty(validateRequest))
            {
                yield return new ValidationResult(validateRequest);
            }
        }
    }
}
