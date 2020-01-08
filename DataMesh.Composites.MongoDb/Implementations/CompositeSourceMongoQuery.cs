using System.Threading.Tasks;

namespace DataMesh.Composites.MongoDb.Implementations
{
    public class CompositeSourceMongoQuery : ICompositeSourceMongoQuery
    {
        private readonly ISimpleMongoStore<MongoSerializableCompositeEntity> Store;

        public CompositeSourceMongoQuery(ISimpleMongoStore<MongoSerializableCompositeEntity> store)
        {
            Store = store;
        }

        public async Task<ICompositeEntity> GetComposite(string resourceId)
        {
            var results = await Store.GetFirst(composite => composite.ResourceId == resourceId);
            return new MongoCompositeEntity()
            {
                ResourceId = results.ResourceId,
                Items = results.Items.Values,
                TypeDefinition = results.TypeDefinition
            };
        }

        
    }
}