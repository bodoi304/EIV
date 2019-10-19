using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eInvoice.MultiLanguages
{
    public class ConfigMultiLanguage
    {
        //Thư viện chứa tất cả các mess phục vụ cho đa ngôn ngữ
        public static Dictionary<String, String> messReturn = new Dictionary<String, String>();
        //Hàm cấu hình load file language theo setting trong Web.config ở project eInvoice.API 
        //<appSettings>
        //<add key = "culture" value=".vn" />
        //<add key = "ForderCulture" value="C:/Users/Administrator/source/repos/eInvoice.API/eInvoice.MultiLanguages/Languages" />
        //</appSettings>
        public static void Configure(String culture, String path)
        {
            String[] allFile = Directory.GetFiles(path);
            foreach (String item in allFile)
            {

                if (item.Contains(culture))
                {
                    LoadConfig(item);
                }
            }
        }
        //Hàm get Mess theo key trong file ConstantsMultiLanguageKey.cs
        public static String getMess(String key)
        {
            return  messReturn[key];
        }

        //Hàm get Mess theo key trong file dùng cho các mess càn dùng String.format
        public static String getMessWithKey(String key)
        {
            return key+ "@" + messReturn[key];
        }

        //Hàm load file theo file mess được cấu hình
        private static void LoadConfig(string settingfile)
        {
            if (File.Exists(settingfile))
            {
                var settingdata = File.ReadAllLines(settingfile);
                for (var i = 0; i < settingdata.Length; i++)
                {
                    var setting = settingdata[i];
                    var sidx = setting.IndexOf("=");
                    if (sidx >= 0)
                    {
                        var skey = setting.Substring(0, sidx);
                        var svalue = setting.Substring(sidx + 1);
                        if (!messReturn.ContainsKey(skey))
                        {
                            messReturn.Add(skey, svalue);
                        }
                    }
                }
            }
        }
    }
}
