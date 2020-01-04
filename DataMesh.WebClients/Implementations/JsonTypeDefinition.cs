using System.Collections.Generic;
using System.Net;
using DataMesh.TypeDefinitions;

namespace DataMesh.Composites
{
    

    // for "Non Partial" Data providers. Those will need a bit more.

    public class JsonTypeDefinition : ITypeDefinition
    {
        public string TypeKey { get; set; }
        public IDictionary<string, ITypeDefinitionItem> Properties { get; set; }
    }
}