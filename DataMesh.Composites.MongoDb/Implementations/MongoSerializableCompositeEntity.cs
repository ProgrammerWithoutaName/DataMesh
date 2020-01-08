using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DataMesh.Composites.MongoDb.Implementations
{
    public class MongoSerializableCompositeEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id;
        public string ResourceId { get; set; }
        public string TypeDefinition { get; set; }
        public Dictionary<string, MongoSourceItem> Items { get; set; }
    }
}