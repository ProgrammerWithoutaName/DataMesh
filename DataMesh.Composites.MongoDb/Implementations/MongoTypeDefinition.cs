using System.Collections.Generic;
using DataMesh.TypeDefinitions;

namespace DataMesh.Composites.MongoDb.Implementations
{
    public class MongoTypeDefinition : ITypeDefinition
    {
        public string TypeKey { get; set; }
        public IDictionary<string, ITypeDefinitionItem> Properties { get; set; }
    }
}