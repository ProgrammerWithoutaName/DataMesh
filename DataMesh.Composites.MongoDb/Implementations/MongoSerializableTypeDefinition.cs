using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DataMesh.Composites.MongoDb.Implementations
{
    public class MongoSerializableTypeDefinition
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string TypeKey { get; set; }

        public Dictionary<string, MongoTypeDefinitionItem> Properties { get; set; }
    }
}