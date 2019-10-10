using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eInvoice.Model
{
    public abstract  class ModelBase
    {
        /// <summary>
        /// check required cho model
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objectVal">object can check required</param>
        /// <param name="arr">mảng chuỗi tên thuộc tính càn check required</param>
        /// <returns>Chuỗi nội dung lỗi</returns>
        public static String validateRequiredObject(ModelBase objRequest, params string[] arr)
        {
            return ModelValidate.validateRequiredObject<ModelBase>(objRequest, arr);
        }

        /// <summary>
        /// check required cho list model
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="lstObject">list object cần check required</param>
        /// <param name="arr">mảng chuỗi tên thuộc tính cần check required</param>
        /// <returns>Chuỗi nội dung lỗi</returns>
        public static String validateRequiredList(List<ModelBase> lstObject, params string[] arr)
        {
            return ModelValidate.validateRequiredList<ModelBase>(lstObject, arr);
        }


        public abstract IMapper mapper();
    }
}
