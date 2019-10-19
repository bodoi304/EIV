using eInvoice.Entity.EDM;
using eInvoice.MultiLanguages;
using eInvoice.Repository.DataAccess;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

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
                    lstError.Add( String.Format(ConfigMultiLanguage.getMessWithKey(ConstantsMultiLanguageKey.E_REQUIRED_001), name[i]));
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
        public static String validateRequiredList<T>(List<T> lstObject, params string[] arr)
        {
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
                            return ConfigMultiLanguage.getMessWithKey(String.Format(ConstantsMultiLanguageKey.E_REQUIRED_002, propertyName));
                        }
                    }
                    if (pro.PropertyType == typeof(DateTime))
                    {
                        DateTime obj = (DateTime)pro.GetValue(lstObject[i], null);
                        if (obj == null || obj == DateTime.MinValue)
                        {
                            return ConfigMultiLanguage.getMessWithKey(String.Format(ConstantsMultiLanguageKey.E_REQUIRED_002, propertyName));
                        }
                    }
                }
            }

            return null;
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

        public static ValidationResult checkUsers(String userName)
        {
            UserDataDA ctlUserData = new UserDataDA();
            userdata users = ctlUserData.checkExist(userName, 0);
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
            if ( dateT.Date  > DateTime .Now.Date  )
            {
                return new ValidationResult(ConstantsMultiLanguageKey.E_FromDate_002);
            }
            else
            {
                return null;
            }
        }

        public static ValidationResult checkDateDenQuaNgayHienTai(DateTime dateT)
        {
            if (dateT.Date > DateTime.Now.Date)
            {
                return new ValidationResult(ConstantsMultiLanguageKey.E_ToDate_002);
            }
            else
            {
                return null;
            }
        }

        public static ValidationResult checkDateTuDenVuotQuaXNgay(int vuotQua,DateTime from,DateTime to)
        {
            if (to.Subtract (from ).Days > vuotQua)
            {
                return new ValidationResult(ConstantsMultiLanguageKey.E_ToDate_003);
            }
            else
            {
                return null;
            }
        }

        public static ValidationResult checkDateDenLonHonTu( DateTime from, DateTime to)
        {
            if (to.Subtract(from).Days < 0)
            {
                return new ValidationResult(ConstantsMultiLanguageKey.E_ToDate_004);
            }
            else
            {
                return null;
            }
        }

        
            
    }
}
