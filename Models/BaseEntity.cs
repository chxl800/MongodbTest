using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MongodbTest.Models
{
    public class BaseEntity
    {
        public ObjectId Id { get; set; }
    }
}