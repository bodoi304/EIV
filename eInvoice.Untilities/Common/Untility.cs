using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.ModelBinding;
using System.Web.Security;
using static eInvoice.Untilities.Common.Constants;

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
        /// <summary>
        /// check gia tri ton tai trong struc
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool checkExistStrucString<T>(String str)
        {
           return typeof(T).GetFields(BindingFlags.Public | BindingFlags.Static).
               Select(x => x.GetRawConstantValue().ToString()).Contains(str);
        }
        public static HashAlgorithm GetHashAlgorithm()
        {

            string temp = Membership.HashAlgorithmType;
            if (temp != "MD5")
            {
                temp = "SHA1";
            }
            HashAlgorithm hashAlgo = HashAlgorithm.Create(temp);
            if (hashAlgo == null)
                return null;
            return hashAlgo;
        }
        /// <summary>
        /// Mã hóa mật khẩu trong us
        /// </summary>
        /// <param name="pass"></param>
        /// <param name="passwordFormat"></param>
        /// <param name="salt"></param>
        /// <returns></returns>
        public static string EncodePassword(string pass, int passwordFormat, string salt)
        {
            //if (passwordFormat != 1) // MembershipPasswordFormat.Clear
            //    return FormsAuthentication.HashPasswordForStoringInConfigFile(pass + salt, "MD5"); ;

            byte[] bIn = Encoding.Unicode.GetBytes(pass);
            byte[] bSalt = Convert.FromBase64String(salt);
            byte[] bRet = null;

            if (passwordFormat == 1)
            {
                // MembershipPasswordFormat.Hashed
                HashAlgorithm hm = GetHashAlgorithm();
                if (hm is KeyedHashAlgorithm)
                {
                    KeyedHashAlgorithm kha = (KeyedHashAlgorithm)hm;
                    if (kha.Key.Length == bSalt.Length)
                    {
                        kha.Key = bSalt;
                    }
                    else if (kha.Key.Length < bSalt.Length)
                    {
                        byte[] bKey = new byte[kha.Key.Length];
                        Buffer.BlockCopy(bSalt, 0, bKey, 0, bKey.Length);
                        kha.Key = bKey;
                    }
                    else
                    {
                        byte[] bKey = new byte[kha.Key.Length];
                        for (int iter = 0; iter < bKey.Length;)
                        {
                            int len = Math.Min(bSalt.Length, bKey.Length - iter);
                            Buffer.BlockCopy(bSalt, 0, bKey, iter, len);
                            iter += len;
                        }
                        kha.Key = bKey;
                    }
                    bRet = kha.ComputeHash(bIn);
                }
                else
                {
                    byte[] bAll = new byte[bSalt.Length + bIn.Length];
                    Buffer.BlockCopy(bSalt, 0, bAll, 0, bSalt.Length);
                    Buffer.BlockCopy(bIn, 0, bAll, bSalt.Length, bIn.Length);
                    bRet = hm.ComputeHash(bAll);
                }
            }

            return Convert.ToBase64String(bRet);
        }

       
        public static string GenerateSalt()
        {
            byte[] buf = new byte[EncodeSalt.SALT_SIZE];
            (new RNGCryptoServiceProvider()).GetBytes(buf);
            return Convert.ToBase64String(buf);
        }

    }
}
