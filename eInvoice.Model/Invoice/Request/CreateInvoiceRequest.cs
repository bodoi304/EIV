using DevExpress.Entity.Model;
using eInvoice.Entity.EDM;
using eInvoice.MultiLanguages;
using eInvoice.Repository.DataAccess;
using eInvoice.Untilities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
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
            AdjustInvDH ctlAdj = new AdjustInvDH();
            Boolean draftCancel = invoice.DraftCancel ?? false;
            //check hoa đơn hủy
            if ((invoice.Type == InvoiceType.Nomal || invoice.Type == InvoiceType.ForReplace || invoice.Type == InvoiceType.ForAdjustAccrete
         || invoice.Type == InvoiceType.ForAdjustReduce
         || invoice.Type == InvoiceType.ForAdjustInfo) && draftCancel)
            {
                List<String> validateRequestHuy = ModelBase.validateRequiredObject(new string[] { "key"
                  ,"invoice.Type","invoice.Status","invoice.DraftCancel","invoice.ComtaxCode"
          }, new object[] { key, invoice.Type, invoice.Status, invoice.DraftCancel, invoice.ComTaxCode });
                foreach (String item in validateRequestHuy)
                {
                    yield return new ValidationResult(item);
                }
                //check invoice co ton tai hay ko
                PVOILInvoice objOut;
                yield return ModelValidate.checkExistNoInvoice(key, out objOut);
                //type=0,1,2,3,4 and status=0 and draftcancel=1 check key họ truyền lên với fkey trong bảng pvoilinvie là status=3 hoặc 5 thì k nhận báo lỗi
                if (objOut != null)
                {
                    if (objOut.Status == Untilities.Common.Constants.InvoiceStatus.Bi_Thay_The
                        || objOut.Status == Untilities.Common.Constants.InvoiceStatus.Xoa_Bo)
                    {
                        yield return new ValidationResult(ConstantsMultiLanguageKey.E_Invoice_Status_3_5);
                    }
                    // Check trong bảng [AdjustInv] nếu đã tồn tại invid của key truyền lên thì báo lỗi
                    AdjustInv objAdj = ctlAdj.CheckExists(objOut.id );
                    if (objAdj != null)
                    {
                        yield return new ValidationResult(String.Format(ConfigMultiLanguage.getMessWithKey(ConstantsMultiLanguageKey.E_AdjustInv_Exist), key));
                    }
                }
           
                //if (invoice.Status != 1)
                //{
                //    yield return new ValidationResult(ConstantsMultiLanguageKey.E_Invoice_Status_Delete);
                //}
            }
            else
            {
                List<String> validateRequest = ModelBase.validateRequiredObject(new string[] { "key"
                 ,"invoice.ComtaxCode","invoice.BusinessDepartmentID" ,"invoice.Type","invoice.Status"
             ,"invoice.PaymentMethod","invoice.PaymentStatus","invoice.CreateDate","invoice.CreateBy"
             ,"invoice.Total"
             ,"invoice.VATRate","invoice.VATAmount","invoice.Amount"
             ,"invoice.AmountInWords"
             ,"invoice.Otherfees"
             ,"invoice.Currency"
             ,"invoice.ExchangeRate"}, new object[] { key
                 ,invoice.ComTaxCode ,invoice.BusinessDepartmentID ,invoice.Type,invoice.Status
             ,invoice.PaymentMethod,invoice.PaymentStatus,invoice.CreateDate,invoice.CreateBy
             ,invoice.Total
             ,invoice.VATRate,invoice.VATAmount,invoice.Amount
             ,invoice.AmountInWords
             ,invoice.OtherFees
             ,invoice.Currency
             ,invoice.ExchangeRate });
                foreach (String item in validateRequest)
                {
                    yield return new ValidationResult(item);
                }

                //ton tai du lieu 1 trong 3 column sau CusTaxCode,CusName,CusAddress thì ca 3 column này phải có data
                if (String.IsNullOrEmpty(invoice.Buyer))
                {

                    List<String> validateRequestCus = ModelBase.validateRequiredObject(new string[] { "invoice.CusTaxCode",
                    "invoice.CusName",
                    "invoice.CusAddress"}, new object[] { invoice.CusTaxCode, invoice.CusName, invoice.CusAddress });
                    foreach (String item in validateRequestCus)
                    {
                        yield return new ValidationResult(item);
                    }


                }


                if (invoice.Status != 0)
                {

                    new ValidationResult(ConstantsMultiLanguageKey.E_Invoice_Status_Create);
                }
                else
                {
                    if ((invoice.Type == InvoiceType.Nomal ||
              invoice.Type == InvoiceType.ForReplace || invoice.Type == InvoiceType.ForAdjustAccrete
              || invoice.Type == InvoiceType.ForAdjustReduce
              || invoice.Type == InvoiceType.ForAdjustInfo) && invoice.DraftCancel != null)
                    {

                        if (!(invoice.Type == InvoiceType.Nomal && !draftCancel))
                        {
                            String noCheckOk = HttpContext.Current.Request.QueryString["NoCheckOK"];
                            if ((!((invoice.Type == InvoiceType.ForAdjustReduce || invoice.Type == InvoiceType.ForAdjustAccrete) && !draftCancel)) || noCheckOk == null)
                            {
                                List<String> validateRequestCus = ModelBase.validateRequiredObject(new string[] { "invoice.originalKey" },
    new object[] { invoice.originalKey });
                                foreach (String item in validateRequestCus)
                                {
                                    yield return new ValidationResult(item);
                                }
                            }
                        }
                        //hoa don ko phai huy
                        if (!draftCancel)
                        {
                            //check invoice co ton tai hay ko
                            yield return ModelValidate.checkExistInvoice(key);
                            if (invoice.Status != 0)
                            {
                                new ValidationResult(ConstantsMultiLanguageKey.E_Invoice_Status_Create);
                            }
                            PVOILInvoiceDA pvOil = new PVOILInvoiceDA();
                            //originalKey trong bảng pvoilinvie là status=3 hoặc 5 thì k nhận báo lỗi
                            PVOILInvoice objInvoice = pvOil.checkExistInvoice(invoice.originalKey);
                            if (objInvoice != null)
                            {
                                if (objInvoice.Status == Untilities.Common.Constants.InvoiceStatus.Bi_Thay_The
                        || objInvoice.Status == Untilities.Common.Constants.InvoiceStatus.Xoa_Bo)
                                {
                                    yield return new ValidationResult(ConstantsMultiLanguageKey.E_Invoice_Status_3_5);
                                }
                            }
                            // type=1,2,3,4 check id của originalKey có trong bảng [AdjustInv] chưa?. nếu có status khác 0,1,2 báo lỗi
                            if ((invoice.Type == InvoiceType.ForReplace || invoice.Type == InvoiceType.ForAdjustAccrete
              || invoice.Type == InvoiceType.ForAdjustReduce
              || invoice.Type == InvoiceType.ForAdjustInfo) && objInvoice != null)
                            {
                                AdjustInv objAdj = ctlAdj.CheckExistsAdj(Convert.ToInt64(objInvoice.id ));
                                if (objAdj!=null && (objAdj.Status != Untilities.Common.Constants.StatusAdj.Adj_0 
                                    && objAdj.Status != Untilities.Common.Constants.StatusAdj.Adj_1 
                                    && objAdj.Status != Untilities.Common.Constants.StatusAdj.Adj_2))
                                {
                                    yield return new ValidationResult(String.Format(ConfigMultiLanguage.getMessWithKey(ConstantsMultiLanguageKey.E_AdjustInv_ThayThe), invoice.originalKey));
                                }
                            }
                            // type=1,2,3,4 thì bắt truyền trường ProcessInvNote => "bắt buộc nhập ProcessInvNote"
                            if (invoice.Type == InvoiceType.ForReplace || invoice.Type == InvoiceType.ForAdjustAccrete
              || invoice.Type == InvoiceType.ForAdjustReduce || invoice.Type == InvoiceType.ForAdjustInfo)
                            {
                                List<String> validateRequestProcessInvNote = ModelBase.validateRequiredObject(new string[] { "invoice.ProcessInvNote" }, 
                                    new object[] { invoice.ProcessInvNote });
                                foreach (String item in validateRequestProcessInvNote)
                                {
                                    yield return new ValidationResult(item);
                                }
                            }
                            //  type=4 : check originalKey nếu tồn tại type=4 thì sẽ chỉ nhận type=4 k nhận type=1,2,3 của fkey mới
                            if (invoice.Type == InvoiceType.ForAdjustInfo)
                            {
                                if (objInvoice.Type == InvoiceType.ForAdjustInfo)
                                {
                                    yield return new ValidationResult(String.Format(ConfigMultiLanguage.getMessWithKey(ConstantsMultiLanguageKey.E_Invoice_DaDieuChinhThongTin), invoice.originalKey));
                                }
                                
                            }
                        }
                        //else if (draftCancel)
                        //{
                        //    if (invoice.Status != 1)
                        //    {
                        //        new ValidationResult(ConstantsMultiLanguageKey.E_Invoice_Status_Delete);
                        //    }
                        //}
                        //check mau hoa don khong được điều chỉnh thay thế 
                        if ((invoice.Type == InvoiceType.ForReplace || invoice.Type == InvoiceType.ForAdjustAccrete
                       || invoice.Type == InvoiceType.ForAdjustReduce
                       || invoice.Type == InvoiceType.ForAdjustInfo) && invoice.DraftCancel != null)
                        {
                            yield return ModelValidate.checkExistInvoiceTemplateTypeView(invoice.Type ?? 8);
                        }
                    }
                    else
                    {
                        //check invoice co ton tai hay ko
                        yield return ModelValidate.checkExistInvoice(key);
                    }
                }



                CustomerDA ctlCustomer = new CustomerDA();
                Customer objCus = ctlCustomer.checkExistCustaxcode(invoice.CusTaxCode);
                if (objCus != null)
                {
                    invoice.CusAddress = objCus.Address;
                    invoice.CusName = objCus.Name;
                }

                WareHouseDA ctlWareHouse = new WareHouseDA();
                Warehouse objWareHouse = ctlWareHouse.checkExistWarehouse(invoice.COutputWarehouseID ?? 0);
                if (objWareHouse != null)
                {
                    invoice.COutputWarehouseCode = objWareHouse.Code;
                    invoice.COutputWarehouse = objWareHouse.Name;
                }
                //check hàng hóa
                List<String> validateRequestProduct = ModelBase.validateRequiredList<ProductModel>(invoice.products,
                    new string[] { "VATRate", "VATAmount" });
                foreach (String item in validateRequestProduct)
                {
                    yield return new ValidationResult(item);
                }

                int ComID = invoice.ComID ?? default(int);

                //Check company
                //yield return ModelValidate.checkComID(ComID);
                //Check buyer
                //yield return ModelValidate.checkExistBuyer(invoice.Buyer);

                yield return ModelValidate.checkValueInArrayValue<InvoiceType>(invoice.Type, "Type", ConstantsMultiLanguageKey.E_InValid_Value);

                yield return ModelValidate.checkValueInArrayValue<PaymentMethod>(invoice.PaymentMethod, "PaymentMethod", ConstantsMultiLanguageKey.E_InValid_Value);

                yield return ModelValidate.checkValueInArrayValue<InvoiceStatus>(invoice.Status, "Status", ConstantsMultiLanguageKey.E_InValid_Value);

                yield return ModelValidate.checkValueInArrayValue<PaymentStatus>(invoice.PaymentStatus, "PaymentStatus", ConstantsMultiLanguageKey.E_InValid_Value);

                yield return ModelValidate.checkValueInArrayValue<VATRate>(invoice.VATRate, "VATRate", ConstantsMultiLanguageKey.E_InValid_Value);

                yield return ModelValidate.checkDoDaiSo(invoice.Total.ToString(), LengthNumber.DO_DAI_19, "Total", ConstantsMultiLanguageKey.E_String_Length);

                yield return ModelValidate.checkSoAm(invoice.Total, "Total", ConstantsMultiLanguageKey.E_Number_Value);

                yield return ModelValidate.checkDoDaiSo(invoice.VATAmount.ToString(), LengthNumber.DO_DAI_19, "VATAmount", ConstantsMultiLanguageKey.E_String_Length);

                yield return ModelValidate.checkSoAm(invoice.VATAmount, "VATAmount", ConstantsMultiLanguageKey.E_Number_Value);

                yield return ModelValidate.checkDoDaiSo(invoice.Amount.ToString(), LengthNumber.DO_DAI_19, "Amount", ConstantsMultiLanguageKey.E_String_Length);

                yield return ModelValidate.checkSoAm(invoice.Amount, "Amount", ConstantsMultiLanguageKey.E_Number_Value);

                yield return ModelValidate.checkDoDaiSo(invoice.AmountInWords, LengthNumber.DO_DAI_255, "AmountInWords", ConstantsMultiLanguageKey.E_String_Length);

                yield return ModelValidate.checkDoDaiSo(invoice.OtherFees.ToString(), LengthNumber.DO_DAI_19, "Otherfees", ConstantsMultiLanguageKey.E_String_Length);

                yield return ModelValidate.checkSoAm(invoice.OtherFees, "Otherfees", ConstantsMultiLanguageKey.E_Number_Value);

                yield return ModelValidate.checkDoDaiSo(invoice.ExchangeRate.ToString(), LengthNumber.DO_DAI_19, "ExchangeRate", ConstantsMultiLanguageKey.E_String_Length);

                yield return ModelValidate.checkSoAm(invoice.ExchangeRate, "ExchangeRate", ConstantsMultiLanguageKey.E_Number_Value);

                yield return ModelValidate.checkDoDaiSo(invoice.Currency, LengthNumber.DO_DAI_3, "Currency", ConstantsMultiLanguageKey.E_String_Length);



                //yield return ModelValidate.checkCurrency(invoice.Currency);

                ///Check BusinessDepartment ID
                BusinessDepartment objBD = null;
                Business objB = null;
                PublishInvoice objPInvoice = null;
                Department objDepartment = null;
                userdata objuser = null;
                //Lấy BusinessID
                yield return ModelValidate.checkExistBussinessDepartment(invoice.BusinessDepartmentID, out objBD);
                if (objBD != null)
                {
                    //Lấy thông tin Business
                    yield return ModelValidate.checkExistBussiness(objBD.BusinessID, out objB);
                    if (invoice.ModifiedDate == null)
                    {
                        invoice.ModifiedDate = DateTime.Now;
                    }
                    if (invoice.PublishDate == null)
                    {
                        invoice.PublishDate = DateTime.Now;
                    }
                    //check khac nhau giua tax code dang nhap va bussinessDepartment
                    if (!invoice.ComTaxCode.Equals(objB.TaxCode))
                    {
                        yield return new ValidationResult(ConstantsMultiLanguageKey.E_TAXCODEDANGNHAP_TAXCODEBUSINESSDEPARTMENT);
                    }
                    //lay tax code
                    invoice.ComTaxCode = objB.TaxCode;
                    if (objB != null)
                    {
                        invoice.Serial = objB.InvSerial;
                        invoice.Pattern = objB.InvPattern;
                        invoice.BusinessID = objBD.BusinessID;
                        //lấy thông tin company
                        CompanyDA ctlCompany = new CompanyDA();
                        Company company = ctlCompany.checkExistByID(objB.ComID);
                        if (company != null)
                        {
                            invoice.ComName = company.Name;
                            invoice.ComAddress = company.Address;
                        }
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
                            if (HttpContext.Current.Request.Headers["Authentication"] != null)
                            {
                                String[] authentication = Untility.decodeBase64(HttpContext.Current.Request.Headers["Authentication"]).Split(':');
                                String userNameLogin = authentication[0];
                                //check khac nhau giua username dang nhap va bussinessDepartment
                                if (!objuser.username.Equals(userNameLogin))
                                {
                                    yield return new ValidationResult(ConstantsMultiLanguageKey.E_USERNAMEDANGNHAP_USERNAMEBUSINESSDEPARTMENT);
                                }
                            }

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
}
