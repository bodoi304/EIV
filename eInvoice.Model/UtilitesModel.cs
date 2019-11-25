using eInvoice.MultiLanguages;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.ModelBinding;
namespace eInvoice.Model
{
    public class UtilitesModel
    {
        //Summary: get error tu model state
        public static String getError(ModelStateDictionary model)
        {
            List<ErrorModel> lstErrors = new List<ErrorModel>();
            foreach (var state in model)
            {
                foreach (var error in state.Value.Errors)
                {
                    ErrorModel item = new ErrorModel();
                    //Check độ dài mess lỗi xem mess có chưa code lỗi không nếu có thì dùng trong chuỗi
                    if (error.ErrorMessage.Length > 25)
                    {
                        String[] strtmp = error.ErrorMessage.Split('@');
                        item.code = strtmp[0];
                        item.message = strtmp[1];
                    }
                    else if(!String.IsNullOrEmpty (error.ErrorMessage))
                    {
                        item.code = error.ErrorMessage;
                        item.message = ConfigMultiLanguage.getMess(error.ErrorMessage);
                    }
                    else
                    {
                        item.code = "Exception";
                        item.message = error.Exception.Message + " " + error.Exception.StackTrace;
                    }
                    lstErrors.Add(item);
                }
            }
            return JsonConvert.SerializeObject(lstErrors);
        }

        //Summary: get error tu model state tra về List<ErrorModel>
        public static List<ErrorModel> getErrorList(ModelStateDictionary model)
        {
            List<ErrorModel> lstErrors = new List<ErrorModel>();
            foreach (var state in model)
            {
                foreach (var error in state.Value.Errors)
                {
                    ErrorModel item = new ErrorModel();
                    //Check độ dài mess lỗi xem mess có chưa code lỗi không nếu có thì dùng trong chuỗi
                    if (error.ErrorMessage.Contains('@'))
                    {
                        String[] strtmp = error.ErrorMessage.Split('@');
                        item.code = strtmp[0];
                        item.message = strtmp[1];
                    }
                    else if (!String.IsNullOrEmpty(error.ErrorMessage))
                    {
                        item.code = error.ErrorMessage;
                        item.message = ConfigMultiLanguage.getMess(error.ErrorMessage);
                    }
                    else
                    {
                        item.code = "Exception";
                        item.message = error.Exception.Message + " " + error.Exception.StackTrace ;
                    }
                    lstErrors.Add(item);
                }
            }
            return lstErrors;
        }
        /// <summary>
        /// get Error theo key multilanguage
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static String getError(String key)
        {
            List<ErrorModel> lstErrors = new List<ErrorModel>();
            ErrorModel item = new ErrorModel();
            item.code = key;
            item.message = ConfigMultiLanguage.getMess(key);
            lstErrors.Add(item);
            return JsonConvert.SerializeObject(lstErrors);
        }
    }
}
