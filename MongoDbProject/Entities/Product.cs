﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoDbProject.Entities
{
    public class Product
    {
        [BsonId] 
        [BsonRepresentation(BsonType.ObjectId)] 
        public string? ProductId { get; set; }    
        public string? Name { get; set; } 
        public int? Stock {  get; set; }  
        public decimal? Price { get; set; }   
        public string? ImageUrl { get; set; } 
    }
}
