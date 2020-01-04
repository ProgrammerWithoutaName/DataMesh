using System.Linq;
using DataMesh.Composites;
using DataMesh.TypeDefinitions;

namespace DataMesh.TypeRegistry.WebService.Mapping
{
    public class TypeDefinitionFactory : ITypeDefinitionFactory
    {
        public TypeDefinition CreateResponse(ITypeDefinition typeDefinition)
            => new TypeDefinition()
            {
                TypeKey = typeDefinition.TypeKey,
                Properties = typeDefinition.Properties.ToDictionary(kv => kv.Key,
                    kv => new TypeDefinitionItem()
                    {
                        TypeKey = kv.Value.TypeKey,
                        Array = kv.Value.Array,
                        Nullable = kv.Value.Nullable,
                        Optional = kv.Value.Optional
                    })
            };

        // TODO: Fix this, ether move away from the definition being explicitly JSON
        // OR, create it's own implementation of the interface.
        public ITypeDefinition CreateSaveable(TypeDefinition typeDefinition)
            => new JsonTypeDefinition()
            {
                TypeKey = typeDefinition.TypeKey,
                Properties = typeDefinition.Properties
                    .ToDictionary(kv => kv.Key,
                        kv => (ITypeDefinitionItem)kv.Value)
            };
    }
}