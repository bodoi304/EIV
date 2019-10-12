using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace eInvoice.Model.Invoice.Request
{
    /// <summary>
    /// Đối tượng Request cho API: api/pvoilbusiness/syncCategory
    /// </summary>
    public class SyncCategoryRequest : ModelBase, IValidatableObject
    {
        public string userName { get; set; }
        public string taxCode { get; set; }

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
           String validateRequest =ModelBase.validateRequiredObject(new string[] { "userName", "taxCode"}, new object[] { userName, taxCode });
            if (!String.IsNullOrEmpty(validateRequest))
            {
                yield return new ValidationResult(validateRequest);
            }
        }
    }
}
