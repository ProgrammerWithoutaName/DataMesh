using DataMesh.TypeDefinitions;

namespace DataMesh.TypeRegistry.WebService.Mapping
{
    public class TypeDefinitionItem : ITypeDefinitionItem
    {
        public string TypeKey { get; set; }
        public bool Nullable { get; set; }
        public bool Optional { get; set; }
        public bool Array { get; set; }
    }
}