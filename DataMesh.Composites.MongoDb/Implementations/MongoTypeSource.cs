﻿using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DataMesh.Composites.MongoDb.Implementations
{
    public class MongoTypeSource : ITypeSource
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string TypeDefinitionKey { get; set; }

        public string SourceKey { get; set; }
        public Uri HealthCheck { get; set; }
        public Uri Retrieve { get; set; }
        public Uri RelinquishOwnership { get; set; }
        public Uri TypeDefinition { get; set; }
        public bool PartialDataProvider { get; set; }
    }

    // Add tests
}