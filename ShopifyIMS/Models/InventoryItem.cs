using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ShopifyIMS.Models
{
    public class InventoryItem
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("price")]
        public double Price { get; set; }

        [BsonElement("qtyInStock")]
        public int QtyInStock { get; set; }

        [BsonElement("storageLocations")]
        public IDictionary<string, int> StorageLocations { get; set; }
    }
}
