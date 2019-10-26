using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eInvoice.Untilities.Common
{
    public class Constants
    {
        public class UserDataType
        {
            public const int ADMIN = 0;
        }

        public class EncodeSalt
        {
            public const int SALT_SIZE =16;
        }


        public class FormatDateTime
        {
            public const String DD_MM_YYYY = "dd/MM/yyyy";
            public const String YYYY_MM_dd = "yyyy/MM/dd";
        }

        public class CategorySync
        {
            public const String KHO = "KHO";
            public const String MAKETOAN = "MAKETOAN";
            public const String DIEMXUAT = "DIEMXUAT";
            public const String NGHIEPVU = "NGHIEPVU";
            public const String ALL = "ALL";
        }

        public struct InvoiceType
        {
            /// <summary>
            /// hóa đơn thông thường
            /// </summary>
            [Description("Hóa đơn thông thường")]
            public const int Nomal = 0;
            /// <summary>
            /// hóa đơn thay thế
            /// </summary>
            [Description("Hóa đơn thay thế")]
            public const int ForReplace = 1;
            /// <summary>
            /// hóa đơn điều chỉnh sai sót
            /// </summary>
            /// <summary>
            /// hóa đơn điều chỉnh sai sót tăng
            /// </summary>
            [Description("Hóa đơn điều chỉnh tăng")]
            public const int ForAdjustAccrete = 2;
            /// <summary>
            /// Điều chỉnh sai sót giảm
            /// </summary>
            [Description("Hóa đơn điều chỉnh giảm")]
            public const int ForAdjustReduce = 3;
            /// <summary>
            /// Điều chỉnh thông tin
            /// </summary>
            [Description("Hóa đơn điều chỉnh thông tin")]
            public const int ForAdjustInfo = 4;
            /// <summary>
            /// Hóa đơn gửi đi phát hành
            /// </summary>
            [Description("Hóa đơn xóa bỏ")]
            public const int ForCancel = 5;
            /// <summary>
            /// truong hop null
            /// </summary>
            public const int Null = -1;
        }

        public struct InvoiceStatus
        {

            [Description("Hóa đơn Dự thảo")]
            public const int Du_Thao = 0;

            [Description("Hóa đơn phát ")]
            public const int Phat_Hanh = 1;

            [Description("Hóa đơn đã kê khai")]
            public const int Da_Ke_Khai = 2;

            [Description("Hóa đơn bị thay thế")]
            public const int Bi_Thay_The = 3;

            [Description("Hóa đơn điều chỉnh thông tin")]
            public const int Bi_Dieu_Chinh = 4;

            [Description("Hóa đơn xóa bỏ")]
            public const int Xoa_Bo = 5;

            public const int Null = -1;
        }

        public struct PaymentMethod
        {


            public const string TM = "TM";



            public const string CK = "CK";



            public const string TM_CK = "TM/CK";


            public const string TTD = "TTD";


            public const string NOI_BO = "Nội bộ";


            public const string BU_TRU = "Bù trừ";

            public const string Null = "";
        }

        public struct PaymentStatus
        {
            public const int CHUA_THANH_TOAN = 0;

            public const int DA_THANH_TOAN = 1;
        }

        public struct LengthNumber
        {
            public const int DO_DAI_19 = 19;
            public const int DO_DAI_3 = 3;
        }

        public struct DateTimeVuotQua
        {
            public const int NGAY_7 = 7;
        }

        public struct VATRate
        {
            public const int PHAN_TRAM_0 = 0;

            public const int PHAN_TRAM_5 = 5;

            public const int PHAN_TRAM_10 = 10;

            public const int KHONG_CHIU_THUE = -1;
        }

        public struct ActiveUser
        {
            public const Boolean ACTIVE = true;

            public const Boolean INACTIVE = false;
        }
    }
}
