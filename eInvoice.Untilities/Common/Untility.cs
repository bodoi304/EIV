using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.ModelBinding;

namespace eInvoice.Untilities.Common
{
    public class Untility
    {
        // Generate a hash for your strings (appends each of the bytes of
        // the value into a single hashed string sử dụng MD5)
        public static String getHashValueOfString(String str)
        {
           return string.Join("", MD5.Create().ComputeHash(Encoding.ASCII.GetBytes(str)).Select(s => s.ToString("x2")));
        }
        //
        //Summary: decode chuỗi base64 encoding UTF8
        public static String decodeBase64(String str)
        {
            byte[] data = Convert.FromBase64String(str);
            string decodedString = Encoding.UTF8.GetString(data);
            return decodedString;
        }

        //Summary: get error tuwf model state
        public static String getError(ModelStateDictionary model)
        {
            var errors = new StringBuilder ();
            foreach (var state in model)
            {
                foreach (var error in state.Value.Errors)
                {
                    errors.Append (error.ErrorMessage + "\n");
                }
            }
            return errors.ToString ();
        }
    }
}
