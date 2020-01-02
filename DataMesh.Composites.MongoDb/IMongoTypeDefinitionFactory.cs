using DataMesh.Composites.MongoDb.Implementations;
using DataMesh.TypeDefinitions;

namespace DataMesh.Composites.MongoDb
{
    public interface IMongoTypeDefinitionFactory
    {
        ITypeDefinition Create(MongoSerializableTypeDefinition source);
        MongoSerializableTypeDefinition CreateSerializable(ITypeDefinition definition);
    }
}