using eInvoice.MultiLanguages;
using System;
using System.Collections.Generic;
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
                        return String.Format(ConfigMultiLanguage.getMess(ConstantsMultiLanguageKey.TRUONG_MAP_JSON_KHONG_DUOC_DE_TRONG_OBJ), propertyName);
                    }
                }
                if (pro.PropertyType == typeof(DateTime))
                {
                    DateTime obj = (DateTime)pro.GetValue(objectVal, null);
                    if (obj == null || obj == DateTime.MinValue)
                    {
                        return String.Format(ConfigMultiLanguage.getMess(ConstantsMultiLanguageKey.TRUONG_MAP_JSON_KHONG_DUOC_DE_TRONG_OBJ), propertyName);
                    }
                }
            }
            return null;
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
                            return ConfigMultiLanguage.getMess(String.Format(ConstantsMultiLanguageKey.TRUONG_MAP_JSON_KHONG_DUOC_DE_TRONG_LIST, propertyName));
                        }
                    }
                    if (pro.PropertyType == typeof(DateTime))
                    {
                        DateTime obj = (DateTime)pro.GetValue(lstObject[i], null);
                        if (obj == null || obj == DateTime.MinValue)
                        {
                            return ConfigMultiLanguage.getMess(String.Format(ConstantsMultiLanguageKey.TRUONG_MAP_JSON_KHONG_DUOC_DE_TRONG_LIST, propertyName));
                        }
                    }
                }
            }

            return null;
        }
    }
}
