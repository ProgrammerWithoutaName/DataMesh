using System.Collections.Generic;

namespace DataMesh.Demo.ItemProviderSource.ItemEditor.Implementations
{
    public class TypeDefinitionResponse
    {
        public string TypeKey { get; set; }
        public Dictionary<string, TypeDefinitionResponseItem> Properties { get; set; }
    }
}