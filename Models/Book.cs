using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MongodbTest.Models
{
    public class Book : BaseEntity
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 作者
        /// </summary>
        public string Author { get; set; }
        /// <summary>
        /// 价格
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// 标签
        /// </summary>
        public List<Tags> Tags { get; set; }
    }
}