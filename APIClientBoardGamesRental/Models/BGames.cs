using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace APIClientBoardGamesRental.Models
{
    public class Rootobject
    {
        public BGames[] Property1 { get; set; }
    }

    public class BGames
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string oid { get; set; }
        [BsonElement("name")]
        [JsonProperty("name")]
        public string name { get; set; }
        public string briefdescribe { get; set; }
        public string image { get; set; }
        public int minPlayers { get; set; }
        public int maxPlayers { get; set; }
        public int playingTime { get; set; }
        public int yearPublished { get; set; }
        public int Rating { get; set; }
        public string type { get; set; }
        public string itemtype { get; set; }
        public int ilosc { get; set; }
        public int cena { get; set; }
        public string describe { get; set; }
    }
}
