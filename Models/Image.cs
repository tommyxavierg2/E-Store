using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace E_Store.Models
{
    public class Image
    {   
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("File")]
        public IFormFile File { get; set; }

        [BsonElement("Source")]
        public string Source { get; set; }

        [BsonElement("Size")]
        public long Size { get; set; }

        [BsonElement("Width")]
        public int Width { get; set; }

        [BsonElement("Height")]
        public int Height { get; set; }

        [BsonElement("Extension")]
        public string Extension { get; set; }
    }
}