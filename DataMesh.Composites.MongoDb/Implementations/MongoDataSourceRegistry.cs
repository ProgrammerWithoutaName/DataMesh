using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataMesh.Composites.MongoDb.Implementations
{
    public class MongoDataSourceRegistry : IMongoDataSourceRegistry
    {
        private readonly ISimpleMongoStore<MongoDataSource> Store;

        public MongoDataSourceRegistry(ISimpleMongoStore<MongoDataSource> store)
        {
            Store = store;
        }

        public async Task<IEnumerable<IDataSource>> GetAllSources()
            => await Store.GetAll();

        public async Task<IDataSource> GetSource(string sourceKey)
            => await Store.GetFirst(source => source.SourceKey == sourceKey);

        public Task RegisterSource(IDataSource dataSource)
            => Store.Set(source => source.SourceKey == dataSource.SourceKey, new MongoDataSource()
            {
                SourceKey = dataSource.SourceKey,
                TypeDefinitionKey = dataSource.TypeDefinitionKey,
                PartialDataProvider = dataSource.PartialDataProvider,
                HealthCheck = dataSource.HealthCheck,
                TypeDefinition = dataSource.TypeDefinition,
                Retrieve = dataSource.Retrieve,
                RelinquishOwnership = dataSource.RelinquishOwnership
            });
    }

    
}