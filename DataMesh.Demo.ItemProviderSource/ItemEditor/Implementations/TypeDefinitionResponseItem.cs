using DataMesh.TypeDefinitions;

namespace DataMesh.Demo.ItemProviderSource.ItemEditor.Implementations
{
    public class TypeDefinitionResponseItem : ITypeDefinitionItem
    {
        public string TypeKey { get; set; }
        public bool Nullable { get; set; }
        public bool Optional { get; set; }
        public bool Array { get; set; }
    }
}