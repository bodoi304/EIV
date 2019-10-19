using eInvoice.Entity.EDM;
using eInvoice.MultiLanguages;
using eInvoice.Repository.DataAccess;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eInvoice.Model.Invoice.Request
{
   public class CreateInvoiceRequest : IValidatableObject
    {
        public String key;
        public InvoicesModel invoice;

        /// <summary>
        /// ham validate model
        /// </summary>
        /// <param name="validationContext"></param>
        /// <returns></returns>
        public IEnumerable<ValidationResult> Validate(System.ComponentModel.DataAnnotations.ValidationContext validationContext)
        {

            List<String> validateRequest = ModelBase.validateRequiredObject(new string[] { "key"
                ,"invoice.ComtaxCode","invoice.BusinessDepartmentID","invoice.CusTaxCode","invoice.CusName","invoice.CusAddress"
           ,"invoice.Buyer" ,"invoice.InvoiceType","invoice.InvoiceStatus"
            ,"invoice.PaymentMethod","invoice.PaymentStatus","invoice.CreateDate","invoice.CreateBy"
            ,"invoice.Total"
            ,"invoice.VATRate","invoice.VATAmount","invoice.Amount"
            ,"invoice.AmountInWords"
            ,"invoice.Otherfees"
            ,"invoice.Currency"
            ,"invoice.ExchangeRate"
            ,"invoice.ComID"}, new object[] { key
                ,invoice.ComTaxCode ,invoice.BusinessDepartmentID,invoice.CusTaxCode,invoice.CusName,invoice.CusAddress
           ,invoice.Buyer ,invoice.Type,invoice.Status
            ,invoice.PaymentMethod,invoice.PaymentStatus,invoice.CreateDate,invoice.CreateBy
            ,invoice.Total
            ,invoice.VATRate,invoice.VATAmount,invoice.Amount
            ,invoice.AmountInWords
            ,invoice.OtherFees
            ,invoice.Currency
            ,invoice.ExchangeRate
            ,invoice.ComID });
            foreach (String item in validateRequest)
            {
                yield return new ValidationResult(item);
            }
            int ComID = invoice.ComID ?? default(int);

            //Check company
            yield return ModelValidate.checkComID(ComID);
            //Check buyer
            yield return ModelValidate.checkExistBuyer (invoice.Buyer);
          
        }
    }
}
