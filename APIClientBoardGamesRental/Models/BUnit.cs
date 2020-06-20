using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace APIClientBoardGamesRental.Models
{
    public class BUnit
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string oid { get; set; }
        public string gameid { get; set; }
        public string dateadded { get; set; }
        public string loaned { get; set; }
        public string dateofrent { get; set; }
        public string barcode { get; set; }
        public string description { get; set; }
    }
}
