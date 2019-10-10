using eInvoice.Model.Category.Response.syncCategory;
using eInvoice.Model.Invoice.Request;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eInvoice.Services.Interface
{
    //các service danh mục
    public interface IInvoiceCategorys
    {
        /// <summary>
        /// Service lấy danh mục trả về cho FAST api/pvoilbusiness/syncCategory
        /// </summary>
        /// <returns></returns>
        SyncCategoryResponse syncCategory(SyncCategoryRequest syncCategory);
    }
}
