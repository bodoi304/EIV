using eInvoice.Entity.EDM;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Xsl;
using static eInvoice.Untilities.Common.Constants;

namespace eInvoice.Untilities.Common
{
    public class GenerateXMLInvoice
    {

        public byte[] GetData(string data)
        {
            XmlDocument xd = new XmlDocument();
            xd.XmlResolver = null;
            xd.PreserveWhitespace = true;
            if (data.StartsWith("File:"))
                xd.Load(data.Substring(data.IndexOf("File:")));
            else
                xd.LoadXml(data);
            //ICertificateService iCert = IoC.Resolve<ICertificateService>();
            //if (xd.GetElementsByTagName("ds:Signature")[0] != null)
            //    xd.GetElementsByTagName("ds:X509Certificate")[0].InnerText = iCert.GetCertFromSerial(xd.GetElementsByTagName("ds:X509Certificate")[0].InnerText).Cert;
            //if (xd.GetElementsByTagName("ds:Signature")[1] != null)
            //    xd.GetElementsByTagName("ds:X509Certificate")[1].InnerText = iCert.GetCertFromSerial(xd.GetElementsByTagName("ds:X509Certificate")[1].InnerText).Cert;

            System.Text.UTF8Encoding _encoding = new System.Text.UTF8Encoding();
            return _encoding.GetBytes(xd.OuterXml);
        }

        private String GetXSLTByTemplate(InvTemplate_GetTemplateInvoice_Result invtemp)
        {
            String xsltData = null;
            if ((invtemp.IsPub ?? false) && !string.IsNullOrWhiteSpace(invtemp.CssData))
            {
                string xslt = invtemp.XsltFile;
                string tmp = "<style type=\"text/css\">";
                StringBuilder sb = new StringBuilder();
                if (!xslt.Contains(tmp))
                    tmp = "<style type=\"text/css\" rel=\"stylesheet\">";
                if (xslt.Contains(tmp))
                {
                    string cssData = invtemp.CssData;
                    string cssLogo = invtemp.CssLogo;
                    string cssBackgr = invtemp.CssBackgr;
                    if (invtemp != null)
                    {
                        cssData = invtemp.CssData; cssLogo = invtemp.CssLogo; cssBackgr = invtemp.CssBackgr;
                    }
                    string head = xslt.Substring(0, xslt.IndexOf(tmp) + tmp.Length);
                    string foot = xslt.Substring(xslt.IndexOf("</style>"));
                    sb.AppendFormat("{0}{1}{2}{3}{4}", head, cssData, cssLogo, cssBackgr, foot);
                }
                var ttt = sb.ToString();
                xsltData = sb.ToString();
            }
            else
                xsltData = invtemp.XsltFile;
            return xsltData;
        }

        public string GetHtml(byte[] invData, InvTemplate_GetTemplateInvoice_Result invtemp)
        {
            XmlDocument xDoc = new XmlDocument();
            xDoc.PreserveWhitespace = true;
            xDoc.LoadXml(System.Text.Encoding.UTF8.GetString(invData));

            StringWriter sw = new StringWriter();
            XslCompiledTransform xct = new XslCompiledTransform();
            XmlProcessingInstruction instruction = xDoc.SelectSingleNode("processing-instruction('xml-stylesheet')") as XmlProcessingInstruction;
            if (xDoc.GetElementsByTagName("Signature")[0] != null)
            {
                byte[] rawCert = Convert.FromBase64String(xDoc.GetElementsByTagName("X509Certificate")[0].InnerText);
                X509Certificate2 cert = new X509Certificate2(rawCert);
                XmlElement xe = xDoc.CreateElement("CNCom");
                Match match = Regex.Match(cert.SubjectName.Name, @"CN=\s*(\w+\s?)+");
                if (match.Success)
                {
                    xe.InnerText = match.Groups[0].Value.Remove(0, 3);
                }
                xDoc.DocumentElement.AppendChild(xe);
                if (xDoc.GetElementsByTagName("Signature")[1] != null)
                {
                    rawCert = Convert.FromBase64String(xDoc.GetElementsByTagName("X509Certificate")[1].InnerText);
                    cert = new X509Certificate2(rawCert);
                    xe = xDoc.CreateElement("CNCus");
                    match = Regex.Match(cert.SubjectName.Name, @"CN=\s*(\w+\s?)+");
                    if (match.Success)
                    {
                        xe.InnerText = match.Groups[0].Value.Remove(0, 3);
                    }
                    xDoc.DocumentElement.AppendChild(xe);
                }
                ImageGenerator.AddCompanyImage(xDoc, true, false);
            }

            if (instruction != null)
            {
                //XmlElement dummyPi = instruction.OwnerDocument.ReadNode(XmlReader.Create(new StringReader("<pi " + instruction.Value + "/>"))) as XmlElement;
                //string href = dummyPi.GetAttribute("href");
                xct.Load(new XmlTextReader(new StringReader(GetXSLTByTemplate(invtemp))));
                xct.Transform(xDoc, null, sw);
            }
            return sw.ToString().Replace("<?xml version=\"1.0\" encoding=\"utf-16\"?>", "");
        }

        /// <summary>
        /// Tạo xml mẫu 01 bán buôn không thực xuất
        /// </summary>
        /// <param name="numTotal"></param>
        /// <returns></returns>
        private StringBuilder Genarate01(PVOILInvoice objInv)
        {
            StringBuilder strbd = new StringBuilder();

            #region Thông tin hóa đơn
            strbd.AppendFormat("<ContractNo>{0}</ContractNo>", objInv.ContractNo);
            strbd.AppendFormat("<ContractDate>{0}</ContractDate>", objInv.ContractDate);
            strbd.AppendFormat("<RefNo>{0}</RefNo>", objInv.RefNo);
            strbd.AppendFormat("<StoreAddress>{0}</StoreAddress>", objInv.COutputWarehouse);
            #endregion

            return strbd;
        }

        /// <summary>
        /// Tạo xml mẫu 03 bán lẻ xăng dầu
        /// </summary>
        /// <param name="numTotal"></param>
        /// <returns></returns>
        private StringBuilder Genarate03(PVOILInvoice objInv)
        {
            StringBuilder strbd = new StringBuilder();

            #region Thông tin điểm bán hàng
            strbd.AppendFormat("<BranchName>{0}</BranchName>", objInv.BranchName);
            strbd.AppendFormat("<BranchAddress>{0}</BranchAddress>", objInv.BranchAddress);
            strbd.AppendFormat("<BranchPhone>{0}</BranchPhone>", objInv.BranchPhone);
            #endregion

            return strbd;
        }

        /// <summary>
        /// Tạo xml mẫu 04 song ngữ hàng hóa dịch vụ
        /// </summary>
        /// <returns></returns>
        private StringBuilder Genarate04(PVOILInvoice objInv)
        {
            StringBuilder strbd = new StringBuilder();
            #region Thông tin hóa đơn
            strbd.AppendFormat("<ContractNo>{0}</ContractNo>", objInv.ContractNo);
            strbd.AppendFormat("<ContractDate>{0}</ContractDate>", objInv.ContractDate);
            strbd.AppendFormat("<RefNo>{0}</RefNo>", objInv.RefNo);
            strbd.AppendFormat("<AmountInWordsEnglish>{0}</AmountInWordsEnglish>", (!string.IsNullOrEmpty(objInv.AmountInWords) && objInv.AmountInWords.Split(';').Count() == 2) ? objInv.AmountInWords.Split(';')[1] : "");
            #endregion

            return strbd;
        }

        private StringBuilder GenarateXMLbyViewer(string viewer, PVOILInvoice objInv)
        {
            switch (viewer)
            {
                case "SONGNGU":
                    return Genarate04(objInv);
                case "BANLEXD":
                    return Genarate03(objInv);
                case "BANBUONKTHUCXUAT":
                    return Genarate01(objInv);
                default:
                    return null;
            }
        }
        public string GetXMLData(PVOILInvoice objInv, List<ProductInv> lstProducts, String tempName)
        {
            int ComID = objInv.ComID ?? 0;
            String Fkey = objInv.Fkey;

            StringBuilder strbd = new StringBuilder();
            strbd.AppendFormat("<?xml-stylesheet type='text/xsl' href='{0}' ?>", "");
            strbd.Append("<Invoice><Content Id=\"SigningData\">");
            int NumTotal = objInv.Currency.ToUpper().Trim() == "VND" ? 0 : 2;
            #region Thông tin cơ bản hóa đơn
            strbd.AppendFormat("<InvoiceName>{0}</InvoiceName>", objInv.Name);
            strbd.AppendFormat("<InvoicePattern>{0}</InvoicePattern>", objInv.Pattern);
            strbd.AppendFormat("<SerialNo>{0}</SerialNo>", objInv.Serial);
            strbd.AppendFormat("<InvoiceNo>{0}</InvoiceNo>", objInv.No);
            DateTime ArisingDate = objInv.ArisingDate ?? DateTime.MinValue;
            strbd.AppendFormat("<ArisingDate>{0}</ArisingDate>", objInv.ArisingDate == DateTime.MinValue ? string.Empty : ArisingDate.ToString("dd/MM/yyyy"));
            strbd.AppendFormat("<SearchKey>{0}</SearchKey>", objInv.SearchKey);
            strbd.AppendFormat("<Fkey>{0}</Fkey>", Fkey);
            strbd.AppendFormat("<Note>{0}</Note>", objInv.Note);
            strbd.AppendFormat("<PaymentMethod>{0}</PaymentMethod>", objInv.PaymentMethod);
            #endregion

            #region Thông tin đơn vị
            strbd.AppendFormat("<ComName>{0}</ComName>", objInv.ComName);
            strbd.AppendFormat("<ComTaxCode>{0}</ComTaxCode>", objInv.ComTaxCode);
            strbd.AppendFormat("<ComAddress>{0}</ComAddress>", objInv.ComAddress);
            strbd.AppendFormat("<ComPhone>{0}</ComPhone>", objInv.ComPhone);
            strbd.AppendFormat("<ComFax>{0}</ComFax>", objInv.ComFax);
            strbd.AppendFormat("<ComBankNo>{0}</ComBankNo>", objInv.ComBankNo);
            strbd.AppendFormat("<ComBankName>{0}</ComBankName>", objInv.ComBankName);
            #endregion

            #region Thông tin khách hàng
            strbd.AppendFormat("<Buyer>{0}</Buyer>", objInv.Buyer);
            strbd.AppendFormat("<CusName>{0}</CusName>", objInv.CusName);
            strbd.AppendFormat("<CusTaxCode>{0}</CusTaxCode>", objInv.CusTaxCode);
            strbd.AppendFormat("<CusPhone>{0}</CusPhone>", objInv.CusPhone);
            strbd.AppendFormat("<CusEmail>{0}</CusEmail>", objInv.CusEmail);
            strbd.AppendFormat("<CusAddress>{0}</CusAddress>", objInv.CusAddress);
            strbd.AppendFormat("<CusBankName>{0}</CusBankName>", objInv.CusBankName);
            strbd.AppendFormat("<CusBankNo>{0}</CusBankNo>", objInv.CusBankNo);
            #endregion

            #region Tạo xml theo từng mẫu
            //tao xml view cho cac kieu ban xang dau
            var strXML = GenarateXMLbyViewer(tempName, objInv);

            strbd.Append(strXML);
            #endregion

            if (objInv.FormatNumber ?? false)
            {
                strbd.AppendFormat("<FormatNumber>{0}</FormatNumber>", objInv.FormatNumber);
            }

            #region Thông tin hàng hóa
            strbd.Append("<Products>");

            var i = 1;
            foreach (ProductInv product in lstProducts)
            {
                strbd.Append("<Product>");
                strbd.AppendFormat("<Order>{0}</Order>", i);
                strbd.AppendFormat("<Extra>{0}</Extra>", Format.ParseChar(product.Extra));
                strbd.AppendFormat("<ProdName>{0}</ProdName>", product.Name);
                int type = objInv.Type ?? 0;
                strbd.AppendFormat("<ProdPrice>{0}</ProdPrice>", (type != (int)InvoiceType.ForAdjustInfo && product.Price > 0) ? Format.NumberVi(product.Price ?? 0, objInv.NumPrice) : "");
                strbd.AppendFormat("<ProdQuantity>{0}</ProdQuantity>", (type != (int)InvoiceType.ForAdjustInfo && product.Quantity > 0) ? Format.NumberVi(product.Quantity ?? 0, objInv.NumQuantity) : "");
                strbd.AppendFormat("<ProdType>{0}</ProdType>", product.ProdType);
                strbd.AppendFormat("<ProdUnit>{0}</ProdUnit>", product.Unit);
                strbd.AppendFormat("<Total>{0}</Total>", product.Total);
                strbd.AppendFormat("<VATAmount>{0}</VATAmount>", product.VATAmount);
                strbd.AppendFormat("<Discount>{0}</Discount>", product.Discount);
                strbd.AppendFormat("<IsSum>{0}</IsSum>", product.IsSum);
                strbd.AppendFormat("<Amount>{0}</Amount>", (objInv.Type != (int)InvoiceType.ForAdjustInfo && product.Amount > 0) ? Format.NumberVi(product.Amount ?? 0, objInv.NumAmount) : "");
                strbd.Append("</Product>");
                i++;
            }
            strbd.Append("</Products>");
            #endregion

            #region Thông tin tổng tiền, tiền thuế
            strbd.AppendFormat("<Total>{0}</Total>", (objInv.Type != (int)InvoiceType.ForAdjustInfo && objInv.Total > 0) ? Format.NumberVi(objInv.Total ?? 0, objInv.NumAmount) : "");
            strbd.AppendFormat("<Otherfees>{0}</Otherfees>", objInv.OtherFees);
            strbd.AppendFormat("<VATRate>{0}</VATRate>", objInv.VATRate);
            strbd.AppendFormat("<VATAmount>{0}</VATAmount>", (objInv.Type != (int)InvoiceType.ForAdjustInfo && objInv.VATAmount > 0) ? Format.NumberVi(objInv.VATAmount ?? 0, objInv.NumAmount) : "");
            strbd.AppendFormat("<Amount>{0}</Amount>", (objInv.Type != (int)InvoiceType.ForAdjustInfo && objInv.Amount > 0) ? Format.NumberVi(objInv.Amount ?? 0, NumTotal) : "");
            strbd.AppendFormat("<AmountInWords>{0}</AmountInWords>", (!string.IsNullOrEmpty(objInv.AmountInWords) && objInv.Amount > 0) ? objInv.AmountInWords.Split(';')[0] : "");
            #endregion

            strbd.Append("</Content></Invoice>");
            XmlDocument xdoc = new XmlDocument();
            xdoc.PreserveWhitespace = true;
            xdoc.LoadXml(strbd.ToString());

            //XmlNode xnd = xdoc.GetElementsByTagName("Invoice")[0].AppendChild(xdoc.CreateElement("qrCodeData"));
            //var qrCodeData = string.Format("{0}|{1};{2};{3};{4};{5};{6};{7};{8};{9}", this.Fkey, this.Pattern, this.Serial, this.No, this.ComTaxCode, this.CusTaxCode, this.Amount, this.VATAmount, this.ArisingDate, this.ComID);
            //xnd.InnerText = qrCodeData;

            return xdoc.OuterXml;
        }
    }
}
