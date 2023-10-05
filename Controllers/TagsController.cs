using MongoDB.Bson;
using MongodbTest.Common;
using MongodbTest.Models;
using System;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace MongodbTest.Controllers
{
    public class TagsController : BaseController
    {
        [HttpGet]
        public ActionResult Add()
        {
            var entity = new Tags() { Tag = "htm", BookId = "Book"};
            var result=  MongoDbHelper.Insert<Tags>(entity);
                return Json(result, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public ActionResult Update()
        {

            Expression<Func<Tags, bool>> filter = u => true;
            filter = filter.And(u => u.Id == new ObjectId("6511278c794131422ffc68eb"));


            //var entity = new Tags() { BookId = "xxxx22", Tag = "html" };
            //MongoDbHelper.Update<Tags>(filter, entity);

            var entity2 = new { BookId = "xxxx666666",Tst="xx"};
 
            var result= MongoDbHelper.UpdateDef(filter, entity2);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Delete()
        {

            Expression<Func<Tags, bool>> filter = u => true;
            filter = filter.And(u => u.Id == new ObjectId("6511278c794131422ffc68e9"));

            var result = MongoDbHelper.Delete<Tags>(filter);

            return Json(result, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public ActionResult GetAll()
        {

            var result = MongoDbHelper.Select<Tags>();

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetBy()
        {

            Expression<Func<Tags, bool>> filter = u => true;
            filter = filter.And(u => u.Tag == "htm");

            //Expression<Func<Tags, Book, bool>> filter = (t, b) => (t.Tag == b.Title);
            //filter = filter.BuildExtendSelectExpre<Tags, Book>(t => t.Tag == "html");



            var result = MongoDbHelper.Select<Tags>(filter);

            //组装
            var data = new Book() { Title = "Title1111", Tags = result };

            return Json(data);
        }


        [HttpPost]
        public ActionResult GetPage()
        {

            Expression<Func<Tags, bool>> filter = u => true;
            Expression<Func<Tags, object>> order = u => u.Tag;

            var result = MongoDbHelper.Select<Tags>(filter, order, 1, 10);

            //return Json(result, JsonRequestBehavior.AllowGet);

            return Json(result);
        }


    }
}