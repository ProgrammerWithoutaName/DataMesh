using DataMesh.Composites.MongoDb.Implementations;
using MongoDB.Driver;

namespace DataMesh.Composites.MongoDb.Configuration
{
    public static class CompositeRegistryConfiguration
    {
        public static IMongoCollection<MongoSerializableCompositeEntity> CreateCompositeEntityStoreClient(MongoStoreDatabaseSettings settings)
            => CollectionClientFactory.CreateCollection<MongoSerializableCompositeEntity>(settings);
    }
}