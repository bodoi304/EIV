﻿using AutoMapper;
using eInvoice.Entity.EDM;
using eInvoice.Model.Category.Response.syncCategory;
using eInvoice.Model.Invoice.Request;
using eInvoice.Repository.DataAccess;
using eInvoice.Repository.Interface;
using eInvoice.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eInvoice.Services.Service
{
    //impliment service danh mục
    public class InvoiceCategorysService : IInvoiceCategorys
    {
        /// <summary>
        /// Service lấy danh mục trả về cho FAST api/pvoilbusiness/syncCategory
        /// </summary>
        /// <returns></returns>
        public SyncCategoryResponse syncCategory(SyncCategoryRequest syncCategory)
        {
            SyncCategoryResponse returnObj = new SyncCategoryResponse();
            SyncCategoryDA ctl = new SyncCategoryDA();

            //Select tất cả danh mục
            List<Warehouse_SelectByTaxCode_Result> lstWH = ctl.Warehouse_SelectByTaxCode(syncCategory.taxCode);
            List<SyncCategory_DiemXuat_Result> lstDX = ctl.SyncCategory_DiemXuat(syncCategory.userName, syncCategory.taxCode);
            List<SyncCategory_NghiepVu_Result> lstNV = ctl.SyncCategory_NghiepVu(syncCategory.userName, syncCategory.taxCode);
            List<AccountingAccount> lstKT = ctl.AccountingAccount_SelectAll();
            //Map danh muc với model
            List<KhoModel> lstKho = (new KhoModel()).mapper().Map<List<Warehouse_SelectByTaxCode_Result>, List<KhoModel>>(lstWH);
            List<DiemXuatModel> lstDiemXuat = (new DiemXuatModel()).mapper().Map<List<SyncCategory_DiemXuat_Result>, List<DiemXuatModel>>(lstDX);
            List<NghiepVuModel> lstNghiepVu = (new NghiepVuModel()).mapper().Map<List<SyncCategory_NghiepVu_Result>, List<NghiepVuModel>>(lstNV);
            List<MaKeToanModel> lstMaKeToan = (new MaKeToanModel()).mapper().Map<List<AccountingAccount>, List<MaKeToanModel>>(lstKT);
            //thiết lập giá trị trả về
            returnObj.result = "true";
            returnObj.MaKeToan.catName = "ACCOUTING_CODE";
            returnObj.MaKeToan.data = lstMaKeToan;
            returnObj.Kho.catName = "WAREHOUSE";
            returnObj.Kho.data = lstKho;
            returnObj.DiemXuat.catName = "DEPARTMENT";
            returnObj.DiemXuat.data = lstDiemXuat;
            returnObj.NghiepVu.catName = "BUSSINESS";
            returnObj.NghiepVu.data = lstNghiepVu;
            return returnObj;
        }
    }
}
