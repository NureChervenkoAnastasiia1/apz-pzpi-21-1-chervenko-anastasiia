﻿using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace TastifyAPI.Entities
{
    public class Order_item
    {
        [BsonId]
        [BsonElement("_id"), BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        [BsonElement("menu_id"), BsonRepresentation(BsonType.String)]
        public string? MenuId { get; set; }

        [BsonElement("order_id"), BsonRepresentation(BsonType.String)]
        public string? OrderId { get; set; }

        [BsonElement("amount"), BsonRepresentation(BsonType.Int32)]
        public Int32? Amount { get; set; }

    }
}
