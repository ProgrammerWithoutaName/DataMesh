using System.Threading.Tasks;

namespace DataMesh.Composites.MongoDb.Implementations
{
    public class CompositeSourceMongoQuery : ICompositeSourceMongoQuery
    {
        private readonly ISimpleMongoStore<ICompositeEntity> Store;

        public CompositeSourceMongoQuery(ISimpleMongoStore<ICompositeEntity> store)
        {
            Store = store;
        }

        public Task<ICompositeEntity> GetComposite(string resourceId)
            => Store.GetFirst(composite => composite.ResourceId == resourceId);
    }
}