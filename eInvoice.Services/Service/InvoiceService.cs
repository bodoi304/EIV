using eInvoice.Entity.EDM;
using eInvoice.Model;
using eInvoice.Model.DTOs.Invoice;
using eInvoice.Model.Invoice;
using eInvoice.Model.Invoice.Request;
using eInvoice.Model.Invoice.Response.createInvoice;
using eInvoice.Model.Invoice.Response.searchInvoice;
using eInvoice.Repository.DataAccess;
using eInvoice.Services.Interface;
using eInvoice.Untilities.Common;
using NReco.PdfGenerator;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Xsl;

namespace eInvoice.Services.Service
{
    public class InvoiceService : IInvoice
    {

        /// <summary>
        /// service tìm kiếm hóa đơn
        /// </summary>
        /// <param name="searchInvoice"></param>
        /// <returns></returns>
        public object searchInvoice(SearchInvoiceRequest searchInvoice)
        {
            try
            {
                SearchInvoiceDA da = new SearchInvoiceDA();
                InvBusinessProcessDA daInvProcess = new InvBusinessProcessDA();
                searchInvoiceResponse response = new searchInvoiceResponse();
                List<PVOILInvoice> lstInvoice = da.selectListInvoice(searchInvoice.maDiemxuatHD, searchInvoice.username, searchInvoice.taxCode, searchInvoice.buyerTaxCode, searchInvoice.from, searchInvoice.to, searchInvoice.type );
                List<InvoicesModel> lstInvoiceModel = ModelBase.mapperStatic<PVOILInvoice, InvoicesModel>().Map<List<PVOILInvoice>, List<InvoicesModel>>(lstInvoice);
                if (searchInvoice .type.ToUpper ().Equals("ORTHER"))
                {
                    foreach (InvoicesModel item in lstInvoiceModel)
                    {
                        searchInvoiceModel tmp = new searchInvoiceModel();
                        InvBusinessProcess objInvProcess= daInvProcess.checkExist(item.id);
                        tmp.key = item.Fkey.ToString();
                        tmp.Pattern = item.Pattern;
                        tmp.Serial = item.Serial;
                        tmp.InvoiceNo = item.No ?? 0;
                        tmp.Soduthao = item.id;
                        int objStatusApprove=0;
                        if (objInvProcess?.StatusApprove == null)
                        {
                            objStatusApprove = 0;
                        }
                        else
                        {
                            objStatusApprove = (bool)objInvProcess?.StatusApprove ? 1 : 0;
                        }
                        tmp.Trangthaikiemtra = objStatusApprove;
                        tmp.Ghichu = objInvProcess?.Comment;
                        response.invoices.Add(tmp);
                    }
                    return response;
                }
                else if (searchInvoice.type.ToUpper().Equals("DC"))
                {
                    foreach (InvoicesModel item in lstInvoiceModel)
                    {
                        InvBusinessProcess objInvProcess = daInvProcess.checkExist(item.id);
                        item.Soduthao = item.id;
                        int objStatusApprove = 0;
                        if (objInvProcess?.StatusApprove == null)
                        {
                            objStatusApprove = 0;
                        }
                        else
                        {
                            objStatusApprove = (bool)objInvProcess?.StatusApprove ? 1 : 0;
                        }
                        item.Trangthaikiemtra = objStatusApprove;
                        item.Ghichu = objInvProcess?.Comment;
                    }
                    return lstInvoiceModel;
                }
                else
                {
                    return null;
                }
             
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        /// <summary>
        /// service insert hóa đơn
        /// </summary>
        /// <param name="createInvoiceModel"></param>
        /// <returns></returns>
        public createInvoiceResponse createInvoice(CreateInvoiceRequest createInvoiceModel)
        {      
            try
            {
                createInvoiceResponse returnObj = new createInvoiceResponse();
                //Gán key của FAST vào FKey
                createInvoiceModel.invoice.Fkey = createInvoiceModel.key;
                CRUDInvoices cRUD = new CRUDInvoices();
                PVOILInvoice invoice = ModelBase.mapperStatic<InvoicesModel, PVOILInvoice>().Map<InvoicesModel, PVOILInvoice>(createInvoiceModel.invoice);
                List<ProductInv> lstProduct =  ModelBase.mapperStatic< ProductModel,  ProductInv >().Map< List<ProductModel>, List< ProductInv >>(createInvoiceModel.invoice.products);
                invoice.InvCateID = 1;
                invoice.SysSource = "Auto";
                int idInvoice= cRUD.insertInvoiceProduct(invoice, lstProduct, createInvoiceModel.invoice.originalKey);
                returnObj.taxCode = createInvoiceModel.invoice.ComTaxCode;
                returnObj.key = createInvoiceModel.invoice.Fkey;
                returnObj.SoDuThao = idInvoice;
                returnObj.result = true;
                return returnObj;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        /// <summary>
        /// service export pdf dự thảo hóa đơn
        /// </summary>
        /// <param name="exportObj"></param>
        /// <returns></returns>
        public byte[] exportPDF(ExportInvoiceRequest exportObj)
        {
            try
            {
                SearchInvoiceDA da = new SearchInvoiceDA();
                GenerateXMLInvoice xMLInvoice = new GenerateXMLInvoice();
                searchInvoiceResponse response = new searchInvoiceResponse();
                //lay hoa don
                PVOILInvoice invoice = da.selectItemInvoiceByFKey(exportObj.FKey);
                InvoicesModel invoiceModel = ModelBase.mapperStatic<PVOILInvoice, InvoicesModel>().Map<PVOILInvoice, InvoicesModel>(invoice);
                searchInvoiceModel tmp = new searchInvoiceModel();
                //tmp.invoice = invoiceModel;
                ////lay san pham
                //tmp.invoice.products = da.selectProductByInvoice(invoiceModel.id);
                //InvTemplate_GetTemplateInvoice_Result template = da.InvTemplate_GetTemplateInvoice(invoice.Pattern, invoice.ComTaxCode);

                ////Tạo data XML từ invoice và product
                //String xml = xMLInvoice.GetXMLData(tmp.invoice, tmp.invoice.products, template.TemplateName);
                //Tạo data html từ xml
                //String html = xMLInvoice.GetHtml(xMLInvoice.GetData(xml), template);
                //Tạo pdf html từ html
                var htmlToPdf = new NReco.PdfGenerator.HtmlToPdfConverter();
                htmlToPdf.Zoom = 1.6f;
                htmlToPdf.Size = NReco.PdfGenerator.PageSize.A4;
                htmlToPdf.Margins = new PageMargins { Left = 20 };
                //byte[] pdfBytes = htmlToPdf.GeneratePdf(html);
                //return pdfBytes;
                return null;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


      
        
    }
}
