using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoDbProject.Entities
{
    public class Category
    {
        [BsonId] //categoryıdnin bir ıd olduğunu bildirmiş olduk
        [BsonRepresentation(BsonType.ObjectId)] //otomatik artan ve ıd değeri üretiyor olması.
        public string? CategoryId { get; set; }
        public string? CategoryName { get; set; }
    }
}
