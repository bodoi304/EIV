using log4net;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace eInvoice.Untilities.Common
{
    public class Format
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(Format));
        #region Định dạng lại tiền theo định dạng tiền Việt
        public static string NumberVi(decimal number, int? countDecimal)
        {
            try
            {
                if (number == 0 && !countDecimal.HasValue)
                {
                    return "0";
                }
                else
                {
                    int count = countDecimal.HasValue ? countDecimal.Value : 2;
                    //string key = new string('#', count - 2);
                    var format = Switch(count);
                    var newFormat = number.ToString(format);
                    var newString = string.Join("", newFormat.Select(x => x == ',' ? '.' :
                                                            x == '.' ? ',' :
                                                            x));
                    return newString;
                }
                
            }
            catch (Exception ex)
            {
                log.Error("Lỗi không thể định dạng số:" + ex);
                return "Lỗi convert";
            }

        }
        #endregion


        #region Định dạng lại tiền theo định dạng tiền quốc tế
        public static string NumberUs(decimal number, int? countDecimal)
        {
            try
            {
                //int count = countDecimal.HasValue ? countDecimal.Value : 2;
                ////string key = new string('#', countDecimal - 2);
                //var format = "##,##.0" + key + "0";
                //var newString = number.ToString(format);
                //return newString;
                return "";
            }
            catch (Exception ex)
            {
                log.Error("Lỗi không thể định dạng số:" + ex);
                return "Lỗi convert";
            }
        }
        #endregion

        private static string Switch(int countDecimal)
        {
            switch (countDecimal)
            {
                case 0:
                    return "##,##0";
                case 1:
                    return "##,##0.0";
                case 2:
                    return "##,##0.00";
                default:
                    string key = new string('#', countDecimal - 2);
                    return "##,##0.0" + key + "0";
            }
        }

        private static string SwitchReport(int countDecimal)
        {
            switch (countDecimal)
            {
                case 0:
                    return "##,##0";
                case 1:
                    return "##,##0.0";
                case 2:
                    return "##,##0.00";
                case 3:
                    return "##,##0.000";
                case 4:
                    return "##,##0.0000";
                default:
                    string key = new string('#', countDecimal - 2);
                    return "##,##0.0" + key + "0";

            }
        }

        public static string FormatNumber(decimal num, int count)
        {
            var format = SwitchReport(count);
            var newFormat = num.ToString(format);
            var newString = string.Join("", newFormat.Select(x => x == ',' ? '.' :
                                                            x == '.' ? ',' :
                                                            x));
            return newString;
        }
        public static string FormatNumberExportExcel(decimal num, int count)
        {
            var format = SwitchReport(count);
            var newFormat = num.ToString(format);
            return newFormat;
        }


        #region Định dạng lại XML chứ ký tự đặc biệt
        public static string ParseChar(string data)
        {
            if (!string.IsNullOrWhiteSpace(data))
            {
                data = data.Replace("&", "&amp;");
                data = data.Replace(">", "&gt;");
                data = data.Replace("<", "&lt;");
                data = data.Replace("%", "&#37;");
                data = data.Replace("'", "&apos;");
                return data;
            }
            return data;
        }
        #endregion

        public static string checkType (int type)
        {
           switch (type)
            {
                case 1:
                    return "Dự thảo thay thế";
                case 2:
                    return "Dự thảo ĐC tăng";
                case 3:
                    return "Dự thảo ĐC giảm";
                case 4:
                    return "Dự thảo ĐC thông tin";
                case 5:
                    return "Dự thảo hủy";
                        default:
                    return "Mới tạo lập";
            }
        }

        
    }
}
