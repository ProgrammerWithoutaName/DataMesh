using DataMesh.TypeDefinitions;

namespace DataMesh.Composites
{
    public class JsonTypeDefinitionItem : ITypeDefinitionItem
    {
        public string TypeKey { get; }
        public bool Nullable { get; }
        public bool Optional { get; }
        public bool Array { get; }
    }
}