using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace E_Store.Models
{
    public class Product
    {   
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Name")]
        public string Name { get; set; }

        [BsonElement("Image")]
        public string Image { get; set; }

        [BsonElement("Description")]
        public string Description { get; set; }

        [BsonElement("Date")]
        public string Date { get; set; }

        [BsonElement("Price")]
        public double Price { get; set; }
        
        [BsonElement("UserId")]
        public string UserId { get; set; }

        [BsonElement("State")]
        public int State { get; set; }
    }
}