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
        /// check required cho model 
        /// </summary>
        /// <param name="name">Tap hop ten các thuộc tính cần required theo thu tự</param>
        /// <param name="arr">các properties cần required</param>
        /// <returns></returns>
        public static String validateRequiredObject( string[] name,object[] arr)
        {
            return ModelValidate.validateRequiredObject(name, arr);
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

        public static IMapper mapperStatic<S,D>()
        {
            var configuration = new MapperConfiguration(cfg => { cfg.CreateMap<S, D>().ReverseMap (); });
            return configuration.CreateMapper();
        }

        public abstract IMapper mapper();
    }
}
