using AutoMapper;
using eInvoice.Entity.EDM;
using eInvoice.Model.Category.Response.syncCategory;
using eInvoice.Model.Invoice.Request;
using eInvoice.Repository.DataAccess;
using eInvoice.Repository.Interface;
using eInvoice.Services.Interface;
using eInvoice.Untilities.Common;
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
        public List<SyncCategoryResponse> syncCategory(SyncCategoryRequest syncCategory)
        {
            try
            {
                List<SyncCategoryResponse> returnObj = new List<SyncCategoryResponse>();
                SyncCategoryResponse Obj = new SyncCategoryResponse();
                SyncCategoryDA ctl = new SyncCategoryDA();
                String catType = syncCategory.CatType.Trim().ToUpper();
                switch (catType)
                {
                    case Constants.CategorySync.DIEMXUAT:
                    case Constants.CategorySync.ALL:
                        //Select  danh mục
                        List<SyncCategory_DiemXuat_Result> lstDX = ctl.SyncCategory_DiemXuat(syncCategory.userName, syncCategory.taxCode);
                        //Map danh muc với model
                        List<DiemXuatModel> lstDiemXuat = (new DiemXuatModel()).mapper().Map<List<SyncCategory_DiemXuat_Result>, List<DiemXuatModel>>(lstDX);
                        Obj = new SyncCategoryResponse();
                        Obj.catName = "DIEMXUAT";
                        Obj.data = lstDiemXuat.Cast<Object>().ToList();
                        returnObj.Add(Obj);
                        if (catType.Equals(Constants.CategorySync.ALL))
                        {
                            goto case Constants.CategorySync.KHO;
                        }
                        else
                        {
                            break;
                        }
                    case Constants.CategorySync.KHO:
                        List<Warehouse_SelectByTaxCode_Result> lstWH = ctl.Warehouse_SelectByTaxCode(syncCategory.taxCode);
                        List<KhoModel> lstKho = (new KhoModel()).mapper().Map<List<Warehouse_SelectByTaxCode_Result>, List<KhoModel>>(lstWH);
                        Obj = new SyncCategoryResponse();
                        Obj.catName = "KHO";
                        Obj.data = lstKho.Cast<Object>().ToList();
                        returnObj.Add(Obj);
                        if (catType.Equals(Constants.CategorySync.ALL))
                        {
                            goto case Constants.CategorySync.MAKETOAN;
                        }
                        else
                        {
                            break;
                        }
                    case Constants.CategorySync.MAKETOAN:
                        List<AccountingAccount> lstKT = ctl.AccountingAccount_SelectAll();
                        List<MaKeToanModel> lstMaKeToan = (new MaKeToanModel()).mapper().Map<List<AccountingAccount>, List<MaKeToanModel>>(lstKT);
                        Obj = new SyncCategoryResponse();
                        Obj.catName = "MAKETOAN";
                        Obj.data = lstMaKeToan.Cast<Object>().ToList();
                        returnObj.Add(Obj);
                        if (catType.Equals(Constants.CategorySync.ALL))
                        {
                            goto case Constants.CategorySync.NGHIEPVU;
                        }
                        else
                        {
                            break;
                        }
                    case Constants.CategorySync.NGHIEPVU:
                        List<SyncCategory_NghiepVu_Result> lstNV = ctl.SyncCategory_NghiepVu(syncCategory.userName, syncCategory.taxCode);
                        List<NghiepVuModel> lstNghiepVu = (new NghiepVuModel()).mapper().Map<List<SyncCategory_NghiepVu_Result>, List<NghiepVuModel>>(lstNV);
                        Obj = new SyncCategoryResponse();
                        Obj.catName = "NGHIEPVU";
                        Obj.data = lstNghiepVu.Cast<Object>().ToList();
                        returnObj.Add(Obj);
                        break;

                    default:
                        break;
                }
                return returnObj;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
