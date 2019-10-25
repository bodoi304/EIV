﻿using DevExpress.Entity.Model;
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
                 ,"invoice.ComtaxCode","invoice.BusinessDepartmentID","invoice.CusTaxCode","invoice.CusName","invoice.CusAddress"
            ,"invoice.Buyer" ,"invoice.InvoiceType","invoice.InvoiceStatus"
             ,"invoice.PaymentMethod","invoice.PaymentStatus","invoice.CreateDate","invoice.CreateBy"
             ,"invoice.Total"
             ,"invoice.VATRate","invoice.VATAmount","invoice.Amount"
             ,"invoice.AmountInWords"
             ,"invoice.Otherfees"
             ,"invoice.Currency"
             ,"invoice.ExchangeRate"
             ,"invoice.ComID","invoice.DepartmentID"}, new object[] { key
                 ,invoice.ComTaxCode ,invoice.BusinessDepartmentID,invoice.CusTaxCode,invoice.CusName,invoice.CusAddress
            ,invoice.Buyer ,invoice.Type,invoice.Status
             ,invoice.PaymentMethod,invoice.PaymentStatus,invoice.CreateDate,invoice.CreateBy
             ,invoice.Total
             ,invoice.VATRate,invoice.VATAmount,invoice.Amount
             ,invoice.AmountInWords
             ,invoice.OtherFees
             ,invoice.Currency
             ,invoice.ExchangeRate
             ,invoice.ComID,invoice.DepartmentID });
            foreach (String item in validateRequest)
            {
                yield return new ValidationResult(item);
            }
            int ComID = invoice.ComID ?? default(int);

            //Check company
            yield return ModelValidate.checkComID(ComID);
            //Check buyer
            yield return ModelValidate.checkExistBuyer(invoice.Buyer);

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

            yield return ModelValidate.checkSoAm(invoice.AmountInWords, "AmountInWords", ConstantsMultiLanguageKey.E_Number_Value);

            yield return ModelValidate.checkDoDaiSo(invoice.OtherFees.ToString(), LengthNumber.DO_DAI_19, "Otherfees", ConstantsMultiLanguageKey.E_String_Length);

            yield return ModelValidate.checkSoAm(invoice.OtherFees, "Otherfees", ConstantsMultiLanguageKey.E_Number_Value);

            yield return ModelValidate.checkDoDaiSo(invoice.ExchangeRate.ToString(), LengthNumber.DO_DAI_19, "ExchangeRate", ConstantsMultiLanguageKey.E_String_Length);

            yield return ModelValidate.checkSoAm(invoice.ExchangeRate, "ExchangeRate", ConstantsMultiLanguageKey.E_Number_Value);

            yield return ModelValidate.checkDoDaiSo(invoice.Currency , LengthNumber.DO_DAI_3, "Currency", ConstantsMultiLanguageKey.E_String_Length);

            yield return ModelValidate.checkCurrency(invoice.Currency);

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
                if (objB != null)
                {
                    invoice.Serial = objB.InvSerial;
                    invoice.Pattern = objB.InvPattern;
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
                        if (objuser.IsApproved ?? true)
                        {
                            yield return new ValidationResult(String.Format(ConfigMultiLanguage.getMessWithKey(ConstantsMultiLanguageKey.E_User_Active), objuser.username));
                        }
                    }
                }
            }

        }
    }
}
