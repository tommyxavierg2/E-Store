using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace E_Store.Models
{
    public class User
    {   
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Name")]
        public string Name { get; set; }

        [BsonElement("LastName")]
        public string LastName { get; set; }

        [BsonElement("Email")]
        public string Email { get; set; }

        [BsonElement("Password")]
        public string Password { get; set; }

        [BsonElement("ConfirmPassword")]
        public string ConfirmPassword { get; set; }
        
        [BsonElement("Phone")]
        public string Phone { get; set; }

        [BsonElement("OptionalPhone")]
        public string OptionalPhone { get; set; }

        [BsonElement("Address")]
        public string Address { get; set; }

        [BsonElement("Client")]
        public bool Client { get; set; }    

        [BsonElement("Business")]
        public bool Business { get; set; }

        [BsonElement("State")]
        public bool State { get; set; }
    }
}