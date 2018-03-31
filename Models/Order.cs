using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace E_Store.Models
{
    public class Order
    {   
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Company")]
        public string Company { get; set; }    

        [BsonElement("UserId")]
        public string UserId { get; set; }

        [BsonElement("Product")]
        public IEnumerable<Product> Product { get; set; }

        [BsonElement("TotalPrice")]
        public double TotalPrice { get; set; }

        [BsonElement("Date")]
        public string Date { get; set; }

        [BsonElement("State")]
        public int State { get; set; }
    }
}