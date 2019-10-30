using DevExpress.Entity.Model;
using eInvoice.Entity.EDM;
using eInvoice.MultiLanguages;
using eInvoice.Repository.DataAccess;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static eInvoice.Untilities.Common.Constants;

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
                 ,"invoice.ComtaxCode","invoice.BusinessDepartmentID" ,"invoice.Type","invoice.Status"
             ,"invoice.PaymentMethod","invoice.PaymentStatus","invoice.CreateDate","invoice.CreateBy"
             ,"invoice.Total"
             ,"invoice.VATRate","invoice.VATAmount","invoice.Amount"
             ,"invoice.AmountInWords"
             ,"invoice.Otherfees"
             ,"invoice.Currency"
             ,"invoice.ExchangeRate"
             ,"invoice.ComID"}, new object[] { key
                 ,invoice.ComTaxCode ,invoice.BusinessDepartmentID ,invoice.Type,invoice.Status
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

            //ton tai du lieu 1 trong 3 column sau CusTaxCode,CusName,CusAddress thì ca 3 column này phải có data
            if (String.IsNullOrEmpty(invoice.Buyer))
            {
               
                List<String> validateRequestCus = ModelBase.validateRequiredObject(new string[] { "invoice.CusTaxCode",
                    "invoice.CusName",
                    "invoice.CusAddress"}, new object[] { invoice.CusTaxCode,invoice.CusName,invoice.CusAddress });
                foreach (String item in validateRequestCus)
                {
                    yield return new ValidationResult(item);
                }


            }
            CustomerDA ctlCustomer = new CustomerDA();
            Customer objCus = ctlCustomer.checkExistCustaxcode(invoice.CusTaxCode);
            if (objCus != null)
            {
                invoice.CusAddress = objCus.Address;
                invoice.CusName = objCus.Name ;
            }

            WareHouseDA ctlWareHouse = new WareHouseDA();
            Warehouse objWareHouse = ctlWareHouse.checkExistWarehouse (invoice.COutputWarehouseID??0);
            if (objWareHouse != null)
            {
                invoice.COutputWarehouseCode = objWareHouse.Code ;
                invoice.COutputWarehouse = objWareHouse.Name;
            }
            //check hàng hóa
            List<String> validateRequestProduct = ModelBase.validateRequiredList<ProductInv>(invoice.products ,
                new string[] {"VATRate","VATAmount"});
            foreach (String item in validateRequestProduct)
            {
                yield return new ValidationResult(item);
            }

            int ComID = invoice.ComID ?? default(int);

            //Check company
            yield return ModelValidate.checkComID(ComID);
            //Check buyer
            //yield return ModelValidate.checkExistBuyer(invoice.Buyer);

            yield return ModelValidate.checkValueInArrayValue<InvoiceType>(invoice.Type, "Type", ConstantsMultiLanguageKey.E_InValid_Value);

            yield return ModelValidate.checkValueInArrayValue<PaymentMethod>(invoice.PaymentMethod , "PaymentMethod", ConstantsMultiLanguageKey.E_InValid_Value);

            yield return ModelValidate.checkValueInArrayValue<InvoiceStatus>(invoice.Status , "Status", ConstantsMultiLanguageKey.E_InValid_Value);

            yield return ModelValidate.checkValueInArrayValue<PaymentStatus>(invoice.PaymentStatus , "PaymentStatus", ConstantsMultiLanguageKey.E_InValid_Value);

            yield return ModelValidate.checkValueInArrayValue<VATRate>(invoice.VATRate , "VATRate", ConstantsMultiLanguageKey.E_InValid_Value);

            yield return ModelValidate.checkDoDaiSo(invoice.Total.ToString (), LengthNumber.DO_DAI_19 , "Total", ConstantsMultiLanguageKey.E_String_Length);

            yield return ModelValidate.checkSoAm(invoice.Total, "Total", ConstantsMultiLanguageKey.E_Number_Value );

            yield return ModelValidate.checkDoDaiSo(invoice.VATAmount.ToString(), LengthNumber.DO_DAI_19, "VATAmount", ConstantsMultiLanguageKey.E_String_Length);

            yield return ModelValidate.checkSoAm(invoice.VATAmount, "VATAmount", ConstantsMultiLanguageKey.E_Number_Value);

            yield return ModelValidate.checkDoDaiSo(invoice.Amount.ToString(), LengthNumber.DO_DAI_19, "Amount", ConstantsMultiLanguageKey.E_String_Length);

            yield return ModelValidate.checkSoAm(invoice.Amount, "Amount", ConstantsMultiLanguageKey.E_Number_Value);

            yield return ModelValidate.checkDoDaiSo(invoice.AmountInWords, LengthNumber.DO_DAI_19, "AmountInWords", ConstantsMultiLanguageKey.E_String_Length);

            yield return ModelValidate.checkDoDaiSo(invoice.OtherFees.ToString(), LengthNumber.DO_DAI_19, "Otherfees", ConstantsMultiLanguageKey.E_String_Length);

            yield return ModelValidate.checkSoAm(invoice.OtherFees, "Otherfees", ConstantsMultiLanguageKey.E_Number_Value);

            yield return ModelValidate.checkDoDaiSo(invoice.ExchangeRate.ToString(), LengthNumber.DO_DAI_19, "ExchangeRate", ConstantsMultiLanguageKey.E_String_Length);

            yield return ModelValidate.checkSoAm(invoice.ExchangeRate, "ExchangeRate", ConstantsMultiLanguageKey.E_Number_Value);

            yield return ModelValidate.checkDoDaiSo(invoice.Currency , LengthNumber.DO_DAI_3, "Currency", ConstantsMultiLanguageKey.E_String_Length);

            //yield return ModelValidate.checkCurrency(invoice.Currency);
            //check invoice co ton tai hay ko
            yield return ModelValidate.checkExistInvoice (key  );
            ///Check BusinessDepartment ID
            BusinessDepartment objBD =null;
            Business objB = null;
            PublishInvoice objPInvoice = null;
            Department objDepartment = null;
            userdata  objuser = null;
            //Lấy BusinessID
            yield return ModelValidate.checkExistBussinessDepartment(invoice.BusinessDepartmentID,out objBD);
            if (objBD != null)
            {
                //Lấy thông tin Business
                yield return ModelValidate.checkExistBussiness(objBD.BusinessID , out objB);
                if (invoice.ModifiedDate == null )
                {
                    invoice.ModifiedDate = DateTime.Now;
                }
                if (invoice.PublishDate == null)
                {
                    invoice.PublishDate  = DateTime.Now;
                }
                if (objB != null)
                {
                    invoice.Serial = objB.InvSerial;
                    invoice.Pattern = objB.InvPattern;
                    invoice.BusinessID = objBD.BusinessID;
                    //Lấy thông tin Publish Invoice
                    yield return ModelValidate.checkExistPublishInvoice(objB.InvSerial, objB.InvPattern, objB.ComID, out objPInvoice);
                    if (objPInvoice != null)
                    {
                        invoice.Name = objPInvoice.InvCateName;
                    }
                    //Lấy thông tin Publish Invoice
                    yield return ModelValidate.checkExistDepartment(objBD.DepartmentID, out objDepartment);
                    if (objDepartment != null)
                    {
                        invoice.DepartmentID = objBD.DepartmentID;
                        invoice.BranchCode = objDepartment.Code;
                        invoice.BranchName = objDepartment.Name;
                        invoice.BranchAddress = objDepartment.Address;
                        invoice.BranchPhone = objDepartment.Phone;
                        invoice.ComID = objDepartment.ComID;
                    }
                    //Lấy thông tin User
                    yield return ModelValidate.checkUsersByID(objBD.UserID, out objuser);
                    if (objuser != null)
                    {
                        if (!objuser.IsApproved ?? !eInvoice.Untilities.Common.Constants.ActiveUser.INACTIVE)
                        {
                            yield return new ValidationResult(String.Format(ConfigMultiLanguage.getMessWithKey(ConstantsMultiLanguageKey.E_User_Active), objuser.username));
                        }
                    }
                }
            }

        }
    }
}
