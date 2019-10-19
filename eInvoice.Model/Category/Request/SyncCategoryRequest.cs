using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using eInvoice.Entity.EDM;
using eInvoice.MultiLanguages;
using eInvoice.Repository.DataAccess;
using eInvoice.Untilities.Common;

namespace eInvoice.Model.Invoice.Request
{
    /// <summary>
    /// Đối tượng Request cho API: api/pvoilbusiness/syncCategory
    /// </summary>
    public class SyncCategoryRequest : ModelBase, IValidatableObject
    {
        public string userName { get; set; }
        public string taxCode { get; set; }
        public string CatType { get; set; }
        
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
           List<String> validateRequest =ModelBase.validateRequiredObject(new string[] { "userName", "taxCode", "CatType" }, new object[] { userName, taxCode, CatType });
            foreach (String item in validateRequest)
            {
                yield return new ValidationResult(item);
            }

            if (!CatType.Trim().ToUpper ().Equals (Constants.CategorySync .ALL) && !CatType.Trim().ToUpper().Equals(Constants.CategorySync.KHO )
               && !CatType.Trim().ToUpper().Equals(Constants.CategorySync.MAKETOAN )
                 && !CatType.Trim().ToUpper().Equals(Constants.CategorySync.DIEMXUAT )
               && !CatType.Trim().ToUpper().Equals(Constants.CategorySync.NGHIEPVU))
            {
                yield return new ValidationResult(ConstantsMultiLanguageKey.E_CAT_001);
            }

            yield return ModelValidate.checkUsers(userName);

            yield return ModelValidate .checkTaxCode (taxCode);
            
        }
    }
}
