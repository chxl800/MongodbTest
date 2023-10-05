using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace MongodbTest.Common
{
    public class BaseController: Controller
    {
        ///// <summary>
        ///// json 处理
        ///// </summary>
        ///// <param name="obj"></param>
        ///// <returns></returns>
        //public new JsonResult Json(object obj)
        //{
        //    //可使用配置处理
        //    string json = JsonConvert.SerializeObject(obj, new JsonSerializerSettings
        //    {
        //        ReferenceLoopHandling = ReferenceLoopHandling.Ignore,//这种方式指定忽略循环引用，是在指定循环级数后忽略，返回的json数据中还是有部分循环的数据
        //        DateFormatString = "yyyy-MM-dd HH:mm:ss",
        //        ContractResolver = new CamelCasePropertyNamesContractResolver()//json中属性开头字母小写的驼峰命名
        //    });

        //    //object data = new
        //    //{
        //    //    data = json
        //    //};

        //    return base.Json(json, JsonRequestBehavior.AllowGet);
        //}



        /// <summary>
        /// 隐藏父类，Json方法，使之返回JsonNetResult类型
        /// </summary>
        protected new JsonResult Json(object data)
        {
            return new JsonNetResult
            {
                Data = data,
            };
        }

        /// <summary>
        /// 隐藏父类，Json方法，使之返回JsonNetResult类型
        /// </summary>
        protected new JsonResult Json(object data, string contentType)
        {
            return new JsonNetResult
            {
                Data = data,
                ContentType = contentType,
            };
        }

        

        /// <summary>
        /// 重写，Json方法，使之返回JsonNetResult类型
        /// </summary>
        protected override JsonResult Json(object data, string contentType, Encoding contentEncoding)
        {
            return new JsonNetResult
            {
                Data = data,
                ContentType = contentType,
                ContentEncoding = contentEncoding,
            };
        }

        /// <summary>
        /// 重写，Json方法，使之返回JsonNetResult类型
        /// </summary>
        protected override JsonResult Json(object data, string contentType, Encoding contentEncoding, JsonRequestBehavior behavior)
        {
            return new JsonNetResult
            {
                Data = data,
                ContentType = contentType,
                ContentEncoding = contentEncoding,
                JsonRequestBehavior = behavior,
            };
        }
    }
}