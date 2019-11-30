using eInvoice.Entity.EDM;
using eInvoice.MultiLanguages;
using eInvoice.Repository.DataAccess;
using eInvoice.Untilities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static eInvoice.Untilities.Common.Constants;

namespace eInvoice.Model
{
    public class ModelValidate
    {
        /// <summary>
        /// check required cho model
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objectVal">object can check required</param>
        /// <param name="arr">mảng chuỗi tên thuộc tính càn check required</param>
        /// <returns>Chuỗi nội dung lỗi</returns>
        public static String validateRequiredObject<T>(T objectVal, params string[] arr)
        {
            foreach (String propertyName in arr)
            {
                PropertyInfo pro = objectVal.GetType().GetProperty(propertyName);
                if (pro.PropertyType == typeof(String))
                {
                    object obj = pro.GetValue(objectVal, null);
                    if (obj == null || String.IsNullOrEmpty(obj.ToString()))
                    {
                        return String.Format(ConfigMultiLanguage.getMess(ConstantsMultiLanguageKey.E_REQUIRED_001), propertyName);
                    }
                }
                if (pro.PropertyType == typeof(DateTime))
                {
                    DateTime obj = (DateTime)pro.GetValue(objectVal, null);
                    if (obj == null || obj == DateTime.MinValue)
                    {
                        return String.Format(ConfigMultiLanguage.getMess(ConstantsMultiLanguageKey.E_REQUIRED_002), propertyName);
                    }
                }
            }
            return null;
        }

        public static List<String> validateRequiredObject(string[] name, params object[] arr)
        {
            List<String> lstError = new List<String>();
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] == null)
                {
                    lstError.Add(String.Format(ConfigMultiLanguage.getMessWithKey(ConstantsMultiLanguageKey.E_REQUIRED_001), name[i]));
                }
                else if (arr[i].GetType() == typeof(String))
                {
                    String objTmp = (String)arr[i];
                    if (objTmp == null || String.IsNullOrEmpty(objTmp))
                    {
                        lstError.Add(String.Format(ConfigMultiLanguage.getMessWithKey(ConstantsMultiLanguageKey.E_REQUIRED_001), name[i]));
                    }
                }
                else if (arr[i].GetType() == typeof(DateTime))
                {
                    DateTime objTmp = (DateTime)arr[i];
                    if (arr[i] == null || objTmp == DateTime.MinValue)
                    {
                        lstError.Add(String.Format(ConfigMultiLanguage.getMessWithKey(ConstantsMultiLanguageKey.E_REQUIRED_001), name[i]));
                    }
                }
                else if (arr[i].GetType() == typeof(int?))
                {
                    int? objTmp = (int?)arr[i];
                    if (objTmp == null)
                    {
                        lstError.Add(String.Format(ConfigMultiLanguage.getMessWithKey(ConstantsMultiLanguageKey.E_REQUIRED_001), name[i]));
                    }
                }
                else if (arr[i].GetType() == typeof(decimal?))
                {
                    decimal? objTmp = (decimal?)arr[i];
                    if (objTmp == null)
                    {
                        lstError.Add(String.Format(ConfigMultiLanguage.getMessWithKey(ConstantsMultiLanguageKey.E_REQUIRED_001), name[i]));
                    }
                }
                else if (arr[i].GetType() == typeof(double?))
                {
                    double? objTmp = (double?)arr[i];
                    if (objTmp == null)
                    {
                        lstError.Add(String.Format(ConfigMultiLanguage.getMessWithKey(ConstantsMultiLanguageKey.E_REQUIRED_001), name[i]));
                    }
                }
            }
            return lstError;
        }

        /// <summary>
        /// check required cho list model
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="lstObject">list object cần check required</param>
        /// <param name="arr">mảng chuỗi tên thuộc tính cần check required</param>
        /// <returns>Chuỗi nội dung lỗi</returns>
        public static List<String> validateRequiredList<T>(List<T> lstObject, params string[] arr)
        {
            List<String> lstError = new List<String>();
            for (int i = 0; i < lstObject.Count; i++)
            {
                foreach (String propertyName in arr)
                {
                    PropertyInfo pro = lstObject[i].GetType().GetProperty(propertyName);
                    if (pro.PropertyType == typeof(String))
                    {
                        object obj = pro.GetValue(lstObject[i], null);
                        if (obj == null || String.IsNullOrEmpty(obj.ToString()))
                        {
                            lstError.Add(ConfigMultiLanguage.getMessWithKey(String.Format(ConstantsMultiLanguageKey.E_REQUIRED_002,i+ 1, propertyName)));
                        }
                    }
                    if (pro.PropertyType == typeof(DateTime))
                    {
                        DateTime obj = (DateTime)pro.GetValue(lstObject[i], null);
                        if (obj == null || obj == DateTime.MinValue)
                        {
                            lstError.Add(ConfigMultiLanguage.getMessWithKey(String.Format(ConstantsMultiLanguageKey.E_REQUIRED_002, i + 1, propertyName)));
                        }
                    }
                    else if (pro.PropertyType == typeof(int?))
                    {
                        int? objTmp = (int?)pro.GetValue(lstObject[i], null);
                        if (objTmp == null)
                        {
                            lstError.Add(String.Format(ConfigMultiLanguage.getMessWithKey(ConstantsMultiLanguageKey.E_REQUIRED_002), i + 1, propertyName));
                        }
                    }
                    else if (pro.PropertyType == typeof(decimal?))
                    {
                        decimal? objTmp = (decimal?)pro.GetValue(lstObject[i], null);
                        if (objTmp == null)
                        {
                            lstError.Add(String.Format(ConfigMultiLanguage.getMessWithKey(ConstantsMultiLanguageKey.E_REQUIRED_002), i + 1, propertyName));
                        }
                    }
                    else if (pro.PropertyType == typeof(double?))
                    {
                        double? objTmp = (double?)pro.GetValue(lstObject[i], null);
                        if (objTmp == null)
                        {
                            lstError.Add(String.Format(ConfigMultiLanguage.getMessWithKey(ConstantsMultiLanguageKey.E_REQUIRED_002), i + 1, propertyName));
                        }
                    }
                }
            }

            return lstError;
        }

        public static ValidationResult checkComID(int ComID)
        {
            CompanyDA ctlCompany = new CompanyDA();
            Company company = ctlCompany.checkExistByID(ComID);
            if (company == null)
            {
                return new ValidationResult(ConstantsMultiLanguageKey.ERR_Common_001);
            }
            else
            {
                return null;
            }

        }

        public static ValidationResult checkTaxCode(String taxCode)
        {
            CompanyDA ctlCompany = new CompanyDA();
            Company company = ctlCompany.checkExistTaxCode(taxCode);
            if (company == null)
            {
                return new ValidationResult(ConstantsMultiLanguageKey.E_COM_100);
            }
            else
            {
                return null;
            }

        }

        public static ValidationResult checkExistBuyer(String buyer)
        {
            CustomerDA ctlCustomer = new CustomerDA();
            Customer cus = ctlCustomer.checkExistBuyer(buyer);
            if (cus == null)
            {
                return new ValidationResult(ConstantsMultiLanguageKey.ERR_Common_002);
            }
            else
            {
                return null;
            }

        }

        public static ValidationResult checkExistCustaxcode(String custaxcode, out Customer objOut)
        {
            CustomerDA ctlCustomer = new CustomerDA();
            objOut = ctlCustomer.checkExistCustaxcode(custaxcode);
            if (objOut == null)
            {
                return new ValidationResult(String.Format(ConfigMultiLanguage.getMessWithKey(ConstantsMultiLanguageKey.E_CustomerTaxCode_Exist), custaxcode) );
            }
            else
            {
                return null;
            }

        }

        public static ValidationResult checkUsers(String userName)
        {
            UserDataDA ctlUserData = new UserDataDA();
            userdata users = ctlUserData.checkExist(userName);
            if (users == null)
            {
                return new ValidationResult(ConstantsMultiLanguageKey.E_EMAIL_100);
            }
            else
            {
                return null;
            }
        }

        public static ValidationResult checkDateTuQuaNgayHienTai(DateTime dateT)
        {
            if (dateT != DateTime.MinValue)
            {
                if (dateT.Date > DateTime.Now.Date)
                {
                    return new ValidationResult(ConstantsMultiLanguageKey.E_FromDate_002);
                }
            }
            return null;
        }

        public static ValidationResult checkDateDenQuaNgayHienTai(DateTime dateT)
        {
            if (dateT != DateTime.MinValue)
            {
                if (dateT.Date > DateTime.Now.Date)
                {
                    return new ValidationResult(ConstantsMultiLanguageKey.E_ToDate_002);
                }
            }
            return null;
        }

        public static ValidationResult checkFormatDate(String dateT, String inputFormat, String nameCheck, String errorCode)
        {
            DateTime d;
            CultureInfo provider = CultureInfo.InvariantCulture;
            if (DateTime.TryParseExact(dateT, inputFormat, provider, DateTimeStyles.None, out d))
            {
                return null;
            }
            else
            {
                return new ValidationResult(String.Format(ConfigMultiLanguage.getMessWithKey(errorCode), nameCheck));
            }
        }

        public static ValidationResult checkDateTuDenVuotQuaXNgay(int vuotQua, DateTime from, DateTime to)
        {
            if (from != DateTime.MinValue && to != DateTime.MinValue)
            {
                if (to.Subtract(from).Days > vuotQua)
                {
                    return new ValidationResult(ConstantsMultiLanguageKey.E_ToDate_003);
                }
            }
            return null;
        }

        public static ValidationResult checkDateDenLonHonTu(DateTime from, DateTime to)
        {
            if (from != DateTime.MinValue && to != DateTime.MinValue)
            {
                if (to.Subtract(from).Days < 0)
                {
                    return new ValidationResult(ConstantsMultiLanguageKey.E_ToDate_004);
                }
            }
            return null;
        }
        /// <summary>
        /// Check gia tri ton tai trong 1 struc hay khong
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objCheck">đối tượng cần check tồn tại</param>
        /// <param name="nameCheck">tên đối tượng cần check tồn tại để trả về</param>
        /// <param name="errorCode">tên mã lỗi</param>
        /// <returns></returns>
        public static ValidationResult checkValueInArrayValue<T>(object objCheck, String nameCheck, String errorCode)
        {
            try
            {
                if (objCheck == null)
                {
                    return new ValidationResult(String.Format(ConfigMultiLanguage.getMessWithKey(errorCode), nameCheck));
                }
                else if (objCheck.GetType() == typeof(int))
                {
                    int invoiceTypeInt = (int)objCheck;
                    if (!Untility.checkExistStrucString<T>(invoiceTypeInt.ToString()))
                    {
                        return new ValidationResult(String.Format(ConfigMultiLanguage.getMessWithKey(errorCode), nameCheck));
                    }
                    else
                    {
                        return null;
                    }
                }
                else if (objCheck.GetType() == typeof(String))
                {
                    String objCheckStr = (String)objCheck;

                    if (!Untility.checkExistStrucString<T>(objCheckStr))
                    {
                        return new ValidationResult(String.Format(ConfigMultiLanguage.getMessWithKey(errorCode), nameCheck));
                    }
                    else
                    {
                        return null;
                    }

                }
                else
                    return null;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public static ValidationResult checkSoAm(object objCheck, String nameCheck, String errorCode)
        {
            if (objCheck == null)
            {
                return new ValidationResult(String.Format(ConfigMultiLanguage.getMessWithKey(errorCode), nameCheck));
            }
            else if (objCheck.GetType() == typeof(String))
            {
                String objCheckStr = (String)objCheck;
                if (String.IsNullOrEmpty(objCheckStr))
                {
                    return new ValidationResult(String.Format(ConfigMultiLanguage.getMessWithKey(errorCode), nameCheck));
                }
                if (Convert.ToDecimal(objCheckStr) < 0)
                {
                    return new ValidationResult(String.Format(ConfigMultiLanguage.getMessWithKey(errorCode), nameCheck));
                }
                else
                {
                    return null;
                }

            }
            else if (objCheck.GetType() == typeof(int))
            {
                int objCheckInt = (int)objCheck;
                if (objCheckInt < 0)
                {
                    return new ValidationResult(String.Format(ConfigMultiLanguage.getMessWithKey(errorCode), nameCheck));
                }
                else
                {
                    return null;
                }

            }
            else
            {
                decimal objCheckDecimal = (decimal)objCheck;

                if (objCheckDecimal < 0)
                {
                    return new ValidationResult(String.Format(ConfigMultiLanguage.getMessWithKey(errorCode), nameCheck));
                }
                else
                {
                    return null;
                }

            }

        }

        public static ValidationResult checkDoDaiSo(String objCheck, int length, String nameCheck, String errorCode)
        {
            if (String.IsNullOrEmpty(objCheck))
            {
                return new ValidationResult(String.Format(ConfigMultiLanguage.getMessWithKey(errorCode), nameCheck, length));
            }

            if (objCheck.Length > length)
            {
                return new ValidationResult(String.Format(ConfigMultiLanguage.getMessWithKey(errorCode), nameCheck, length));
            }
            else
            {
                return null;
            }
        }

        public static ValidationResult checkCurrency(String code)
        {
            CurrencyDA dACur = new CurrencyDA();
            Currency obj = dACur.checkExist(code);
            if (obj == null)
            {
                return new ValidationResult(ConstantsMultiLanguageKey.E_Currency_NotExist);
            }
            return null;
        }


        public static ValidationResult checkExistBussinessDepartment(int BusinessDepartmentID, out BusinessDepartment objOut)
        {
            BusinessDepartmentDA dABu = new BusinessDepartmentDA();
            objOut = dABu.checkExistByID(BusinessDepartmentID);
            if (objOut == null)
            {
                return new ValidationResult(ConstantsMultiLanguageKey.E_BusinessDepartmentID_NotExist);
            }
            return null;
        }


        public static ValidationResult checkExistBussiness(int BusinessID, out Business objOut)
        {
            BussinessDA dABu = new BussinessDA();
            objOut = dABu.checkExistByID(BusinessID);
            if (objOut == null)
            {
                return new ValidationResult(ConstantsMultiLanguageKey.E_BusinessID_NotExist);
            }
            return null;
        }

        public static ValidationResult checkExistPublishInvoice(String invSerial, String invPattern, int comID, out PublishInvoice objOut)
        {
            PublishInvoiceDA dABu = new PublishInvoiceDA();
            objOut = dABu.checkExistByID(invSerial, invPattern, comID);
            if (objOut == null)
            {
                return new ValidationResult(ConstantsMultiLanguageKey.E_PublishInvoice_NotExist);
            }
            return null;
        }

        public static ValidationResult checkExistDepartment(int departmentID, out Department objOut)
        {
            DepartmentDA dADepart = new DepartmentDA();
            objOut = dADepart.checkExistByID(departmentID);
            if (objOut == null)
            {
                return new ValidationResult(ConstantsMultiLanguageKey.E_Department_NotExist);
            }
            return null;
        }

        public static ValidationResult checkUsersByID(int userID, out userdata objOut)
        {
            UserDataDA ctlUserData = new UserDataDA();
            objOut = ctlUserData.checkExistByUserID(userID);
            if (objOut == null)
            {
                return new ValidationResult(ConstantsMultiLanguageKey.E_EMAIL_100);
            }
            else
            {
                return null;
            }
        }

        public static ValidationResult checkExistInvoice(String fkey)
        {
            PVOILInvoiceDA pvOil = new PVOILInvoiceDA();
            PVOILInvoice objInv = pvOil.checkExistInvoice(fkey);
            if (objInv != null)
            {
                return new ValidationResult(String.Format(ConfigMultiLanguage.getMessWithKey(ConstantsMultiLanguageKey.E_Invoice_Exist), fkey));
            }
            else
            {
                return null;
            }
        }

        public static ValidationResult checkExistNoInvoice(String fkey, out PVOILInvoice objOut)
        {
            PVOILInvoiceDA pvOil = new PVOILInvoiceDA();
            PVOILInvoice objInv = pvOil.checkExistInvoice(fkey);
            if (objInv == null)
            {
                objOut = null;
                return new ValidationResult(ConfigMultiLanguage.getMess(ConstantsMultiLanguageKey.E_InvoiceDelete_NotExist));
            }
            else
            {
                objOut = objInv;
                return null;
            }
        }

        public static ValidationResult checkExistInvoiceTemplateTypeView(int Type)
        {
            InvTemplateDA ctlInvTemp = new InvTemplateDA();
            InvTemplate objInv = ctlInvTemp.checkExistTypeView(Type, "XKTX");
            if (objInv != null)
            {
                return new ValidationResult(ConstantsMultiLanguageKey.E_InvoiceTemplate_Type );
            }
            else
            {
                return null;
            }
        }
    }
}
