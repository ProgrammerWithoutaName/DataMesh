using DataMesh.TypeDefinitions;

namespace DataMesh.Composites.MongoDb.Implementations
{
    public class MongoTypeDefinitionItem : ITypeDefinitionItem
    {
        public string TypeKey { get; set; }
        public bool Nullable { get; set; }
        public bool Optional { get; set; }
        public bool Array { get; set; }
    }
}