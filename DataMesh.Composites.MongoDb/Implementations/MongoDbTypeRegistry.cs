using System.Threading.Tasks;
using DataMesh.TypeDefinitions;

namespace DataMesh.Composites.MongoDb.Implementations
{
    public class MongoDbTypeRegistry : IMongoDbTypeRegistry
    {
        private readonly ISimpleMongoStore<ITypeDefinition> TypeStore;

        public MongoDbTypeRegistry(ISimpleMongoStore<ITypeDefinition> typeStore)
        {
            TypeStore = typeStore;
        }

        public async Task<ITypeDefinition> GetDefinition(string typeKey)
            => await TypeStore.GetFirst(type => type.TypeKey == typeKey);
        public async Task SetDefinition(ITypeDefinition typeDefinition)
            => await TypeStore.Set(type => type.TypeKey == typeDefinition.TypeKey, typeDefinition);
    }
}