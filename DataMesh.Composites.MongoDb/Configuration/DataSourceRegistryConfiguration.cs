using DataMesh.Composites.MongoDb.Implementations;
using MongoDB.Driver;

namespace DataMesh.Composites.MongoDb.Configuration
{
    public static class DataSourceRegistryConfiguration 
    {
        public static IMongoCollection<MongoDataSource> CreateTypeDefinitionStoreClient(
            MongoStoreDatabaseSettings settings)
            => CollectionClientFactory.CreateCollection<MongoDataSource>(settings);
    }
}