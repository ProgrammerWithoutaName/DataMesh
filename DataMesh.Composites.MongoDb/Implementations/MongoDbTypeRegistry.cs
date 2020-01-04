using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataMesh.TypeDefinitions;

namespace DataMesh.Composites.MongoDb.Implementations
{
    // TODO: Likely need to fix this part. The Auth token is important, but accidentally let the web api layer leak into the datastore layer
    public class MongoDbTypeRegistry : IMongoDbTypeRegistry
    {
        private readonly ISimpleMongoStore<MongoSerializableTypeDefinition> TypeStore;
        private readonly IMongoTypeDefinitionFactory Factory;

        public MongoDbTypeRegistry(ISimpleMongoStore<MongoSerializableTypeDefinition> typeStore, IMongoTypeDefinitionFactory factory)
        {
            TypeStore = typeStore;
            Factory = factory;
        }

        public async Task<IEnumerable<ITypeDefinition>> GetDefinitions(string authToken)
        {
            var results = await TypeStore.GetAll();
            return results.Select(Factory.Create);
        }

        public async Task<ITypeDefinition> GetDefinition(string typeKey, string authToken)
            => Factory.Create(await TypeStore.GetFirst(type => type.TypeKey == typeKey));

        public async Task SetDefinition(ITypeDefinition typeDefinition, string authToken)
            => await TypeStore.Set(type => type.TypeKey == typeDefinition.TypeKey,
                Factory.CreateSerializable(typeDefinition));
    }
}