using System.Collections.Generic;

namespace DataMesh.TypeRegistry.WebService.Mapping
{
    public class TypeDefinition
    {
        public string TypeKey { get; set; }
        public Dictionary<string, TypeDefinitionItem> Properties { get; set; }
    }
}