using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace eInvoice.Untilities.Common
{
   public class ImageGenerator
    {
        public static void AddCompanyImage(XmlDocument xDoc, bool verifyStatus, bool certUnknown)
        {
            XmlElement xe = xDoc.CreateElement("image");
            xe.InnerText = "Signature Valid";
            xe.SetAttribute("URI", ImageEmbed());
            xDoc.DocumentElement.AppendChild(xe);
        }
        public static void AddCustomerImage(XmlDocument xDoc)
        {
            XmlElement xe = xDoc.CreateElement("imageClient");
            xe.InnerText = "Signature Valid";
            xe.SetAttribute("URI", ImageEmbed());
            xDoc.DocumentElement.AppendChild(xe);
        }
        private static string ImageEmbed()
        {
            byte[] data = File.ReadAllBytes(AppDomain.CurrentDomain.BaseDirectory + "/Content/img/t.png");
            return "data:image/png;base64," + Convert.ToBase64String(data);
        }
    }
}
