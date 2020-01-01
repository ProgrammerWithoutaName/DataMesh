using System.Threading.Tasks;

namespace DataMesh.Composites.MongoDb
{
    public class MongoDataSourceRegistry : IMongoDataSourceRegistry
    {
        private readonly ISimpleMongoStore<ITypeSource> Store;

        public MongoDataSourceRegistry(ISimpleMongoStore<ITypeSource> store)
        {
            Store = store;
        }

        public Task<ITypeSource> GetSource(string sourceKey)
            => Store.GetFirst(source => source.SourceKey == sourceKey);

        public Task RegisterSource(ITypeSource typeSource)
            => Store.Set(source => source.SourceKey == typeSource.SourceKey, typeSource);
    }
}