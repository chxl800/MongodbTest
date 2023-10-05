using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MongodbTest.Models
{
    public class Tags : BaseEntity
    {
        /// <summary>
        /// 标签
        /// </summary>
        public string Tag { get; set; }
        /// <summary>
        /// 对应的书的Id
        /// </summary>
        public string BookId { get; set; }
    }
}