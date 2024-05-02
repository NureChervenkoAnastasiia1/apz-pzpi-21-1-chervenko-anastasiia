using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace TastifyAPI.DTOs.CreateDTOs
{
    public class RestaurantCreateDTO
    {
        /*public string Name { get; set; }
        public string Address { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }
        public string Info { get; set; }
        public List<string> Cuisine { get; set; }*/

        [BsonElement("name"), BsonRepresentation(BsonType.String)]
        public string? Name { get; set; }

        [BsonElement("address"), BsonRepresentation(BsonType.String)]
        public string? Address { get; set; }

        [BsonElement("telephone"), BsonRepresentation(BsonType.String)]
        public string? Telephone { get; set; }

        [BsonElement("email"), BsonRepresentation(BsonType.String)]
        public string? Email { get; set; }

        [BsonElement("info"), BsonRepresentation(BsonType.String)]
        public string? Info { get; set; }

        [BsonElement("cuisine")]
        public List<string>? Cuisine { get; set; }
    }
}
