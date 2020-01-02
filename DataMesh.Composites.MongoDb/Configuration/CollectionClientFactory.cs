using MongoDB.Driver;

namespace DataMesh.Composites.MongoDb.Configuration
{
    internal static class CollectionClientFactory
    {
        internal static IMongoCollection<T> CreateCollection<T>(MongoStoreDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            var collection = database.GetCollection<T>(settings.CollectionName);
            return collection;
        }
    }
}