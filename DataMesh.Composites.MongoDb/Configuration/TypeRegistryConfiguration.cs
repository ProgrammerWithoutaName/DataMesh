using DataMesh.Composites.MongoDb.Implementations;
using DataMesh.TypeDefinitions;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace DataMesh.Composites.MongoDb.Configuration
{
    public static class TypeRegistryConfiguration
    {
        public static IMongoCollection<MongoSerializableTypeDefinition> CreateTypeDefinitionStoreClient(
            MongoStoreDatabaseSettings settings)
            => CollectionClientFactory.CreateCollection<MongoSerializableTypeDefinition>(settings);
    }
}