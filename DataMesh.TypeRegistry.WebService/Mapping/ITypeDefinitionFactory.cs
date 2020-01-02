using DataMesh.TypeDefinitions;

namespace DataMesh.TypeRegistry.WebService.Mapping
{
    public interface ITypeDefinitionFactory
    {
        TypeDefinition CreateResponse(ITypeDefinition typeDefinition);

        ITypeDefinition CreateSaveable(TypeDefinition typeDefinition);
    }
}