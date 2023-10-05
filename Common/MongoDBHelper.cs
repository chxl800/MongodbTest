using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace MongodbTest.Common
{

    /// <summary>
    /// MongoDb帮助类
    /// </summary>
    public class MongoDbHelper
    {
        //connStr=mongodb://[username:password@]host1[:port1][,host2[:port2],…[,hostN[:portN]]][/[database][?options]]
        //数据库连接字符串，如：mongodb://sa:123456@localhost:27017
        private static readonly string connStr = "mongodb://127.0.0.1:27017";
        private static readonly string dbName = "test";
        private static IMongoDatabase db = null;
        private static readonly object lockHelper = new object();
        private MongoDbHelper() { }
        public static IMongoDatabase GetDb()
        {
            if (db == null)
            {
                lock (lockHelper)
                {
                    if (db == null)
                    {
                        var client = new MongoClient(connStr);
                        db = client.GetDatabase(dbName);
                    }
                }
            }
            return db;
        }


        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static T Insert<T>(T entity)
        {
            db = GetDb();
            db.GetCollection<T>(typeof(T).Name).InsertOne(entity);
            return entity;
        }

        /// <summary>
        /// 更新数据-匿名字段
        /// </summary>
        public static UpdateResult UpdateDef<T>(Expression<Func<T, bool>> filter, dynamic t)
        {
            db = GetDb();

            //要修改的字段
            var list = new List<UpdateDefinition<T>>();
            foreach (var tt in typeof(T).GetProperties())//实体字段
            {
                if (tt.Name.ToLower() == "id") continue;//跳过id

                foreach (var item in t.GetType().GetProperties())//指定字段
                {
                    if (item.Name.ToLower() == tt.Name.ToLower()) //指定字段==实体字段
                    {
                        list.Add(Builders<T>.Update.Set(item.Name, item.GetValue(t)));
                    }
                }
            }
            var updatefilter = Builders<T>.Update.Combine(list);

            return db.GetCollection<T>(typeof(T).Name).UpdateMany(filter, updatefilter);
        }
        /// <summary>
        /// 更新数据
        /// </summary>
        public static UpdateResult Update<T>(Expression<Func<T, bool>> filter, T t)
        {
            db = GetDb();

            //要修改的字段
            var list = new List<UpdateDefinition<T>>();
            foreach (var item in t.GetType().GetProperties())//实体字段
            {
                if (item.Name.ToLower() == "id") continue;//跳过id
                list.Add(Builders<T>.Update.Set(item.Name, item.GetValue(t)));
            }
            var updatefilter = Builders<T>.Update.Combine(list);

           return db.GetCollection<T>(typeof(T).Name).UpdateMany(filter, updatefilter);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static DeleteResult Delete<T>(Expression<Func<T, bool>> filter)
        {
            db = GetDb();
            return db.GetCollection<T>(typeof(T).Name).DeleteMany(filter); 
        }

        /// <summary>
        /// 查询所有数据
        /// </summary>
        /// <returns></returns>
        public static List<T> Select<T>()
        {
            db = GetDb();
            return db.GetCollection<T>(typeof(T).Name).Find(_ => true).ToList();
        }

        /// <summary>
        /// 查询条件数据
        /// </summary>
        /// <returns></returns>
        public static List<T> Select<T>(Expression<Func<T, bool>> filter)
        {
            db = GetDb();
            return db.GetCollection<T>(typeof(T).Name).Find(filter).ToList();
        }

        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <returns></returns>
        public static List<T> Select<T>(Expression<Func<T, bool>> filter, Expression<Func<T, object>> order, int pageIndex, int pageSize, string ascOrDesc = "desc")
        {
            db = GetDb();

            if (ascOrDesc.ToLower() == "asc")
            {
                return db.GetCollection<T>(typeof(T).Name).AsQueryable<T>().Where(filter).OrderBy(order).Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList();
            }
            else {
                return db.GetCollection<T>(typeof(T).Name).AsQueryable<T>().Where(filter).OrderByDescending(order).Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList();
            }
           
        }

        /// <summary>
        /// 查询排序数据
        /// </summary>
        /// <returns></returns>
        public static List<T> Select<T>(Expression<Func<T, bool>> filter, Expression<Func<T, object>> order=null, string ascOrDesc = "desc")
        {
            db = GetDb();

            if (order != null)
            {
                if (ascOrDesc.ToLower() == "asc")
                {
                    return db.GetCollection<T>(typeof(T).Name).AsQueryable<T>().Where(filter).OrderBy(order).ToList();
                }
                else
                {
                    return db.GetCollection<T>(typeof(T).Name).AsQueryable<T>().Where(filter).OrderByDescending(order).ToList();
                }
            }
            else{
                return db.GetCollection<T>(typeof(T).Name).Find(filter).ToList();
            }
        }
    }
}