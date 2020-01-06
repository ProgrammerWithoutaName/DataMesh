using DataMesh.TypeDefinitions;

namespace DataMesh.Demo.ItemProviderSource.ItemEditor.Implementations
{
    public class ItemEditorTypeDefinitionItem : ITypeDefinitionItem
    {
        public ItemEditorTypeDefinitionItem(string propertyName)
        {
            // Note: this is a hack- would not suggest this in a real implementation.
            TypeKey = propertyName == "Price" ? "number" : "string";
            Nullable = false;
            Optional = false;
            Array = false;
        }
        public string TypeKey { get; set; }
        public bool Nullable { get; set; }
        public bool Optional { get; set; }
        public bool Array { get; set; }
    }
}