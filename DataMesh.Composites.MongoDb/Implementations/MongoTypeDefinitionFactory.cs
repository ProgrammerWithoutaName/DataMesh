using System.Linq;
using DataMesh.TypeDefinitions;

namespace DataMesh.Composites.MongoDb.Implementations
{
    public class MongoTypeDefinitionFactory : IMongoTypeDefinitionFactory
    {
        public ITypeDefinition Create(MongoSerializableTypeDefinition source)
            => new MongoTypeDefinition()
            {
                TypeKey = source.TypeKey,
                Properties = source.Properties
                    .ToDictionary(
                        kv => kv.Key, 
                        kv => (ITypeDefinitionItem)kv.Value)
            };

        public MongoSerializableTypeDefinition CreateSerializable(ITypeDefinition definition)
            => new MongoSerializableTypeDefinition()
            {
                TypeKey = definition.TypeKey,
                Properties = definition.Properties
                    .ToDictionary(
                        kv => kv.Key,
                        kv => new MongoTypeDefinitionItem()
                        {
                            TypeKey = kv.Value.TypeKey,
                            Array = kv.Value.Array,
                            Nullable = kv.Value.Nullable,
                            Optional = kv.Value.Optional
                        })
            };
    }
}