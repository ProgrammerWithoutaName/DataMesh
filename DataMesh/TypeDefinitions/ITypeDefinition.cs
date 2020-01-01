using System.Collections.Generic;

namespace DataMesh.TypeDefinitions
{
    public interface ITypeDefinition
    {
        string TypeKey { get; }
        IDictionary<string, ITypeDefinitionItem> Properties { get; }
    }
}