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
        public class StatusUserAPIAccess
        {
            public const int ACTIVE = 1;
            public const int NONACTIVE = 0;
        }

        public class CategorySync
        {
            public const String KHO = "KHO";
            public const String MAKETOAN = "MAKETOAN";
            public const String DIEMXUAT = "DIEMXUAT";
            public const String NGHIEPVU = "NGHIEPVU";
            public const String ALL = "ALL";
        }

        public enum InvoiceType
        {
            /// <summary>
            /// hóa đơn thông thường
            /// </summary>
            [Description("Hóa đơn thông thường")]
            Nomal,
            /// <summary>
            /// hóa đơn thay thế
            /// </summary>
            [Description("Hóa đơn thay thế")]
            ForReplace,
            /// <summary>
            /// hóa đơn điều chỉnh sai sót
            /// </summary>
            /// <summary>
            /// hóa đơn điều chỉnh sai sót tăng
            /// </summary>
            [Description("Hóa đơn điều chỉnh tăng")]
            ForAdjustAccrete,
            /// <summary>
            /// Điều chỉnh sai sót giảm
            /// </summary>
            [Description("Hóa đơn điều chỉnh giảm")]
            ForAdjustReduce,
            /// <summary>
            /// Điều chỉnh thông tin
            /// </summary>
            [Description("Hóa đơn điều chỉnh thông tin")]
            ForAdjustInfo,
            /// <summary>
            /// Hóa đơn gửi đi phát hành
            /// </summary>
            [Description("Hóa đơn xóa bỏ")]
            ForCancel,
            /// <summary>
            /// truong hop null
            /// </summary>
            Null = -1,
        }

        public enum InvoiceStatus
        {
      
            [Description("Hóa đơn Dự thảo")]
            Du_Thao = 1,

            [Description("Hóa đơn phát ")]
            Phat_Hanh = 2,

            [Description("Hóa đơn đã kê khai")]
            Da_Ke_Khai = 3,

            [Description("Hóa đơn bị thay thế")]
            Bi_Thay_The = 4,

            [Description("Hóa đơn điều chỉnh thông tin")]
            Bi_Dieu_Chinh = 5,

            [Description("Hóa đơn xóa bỏ")]
            Xoa_Bo = 6,

            Null = -1,
        }
    }
}
