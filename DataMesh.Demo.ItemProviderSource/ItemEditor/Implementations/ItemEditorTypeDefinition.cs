using System.Collections.Generic;
using System.Linq;
using DataMesh.TypeDefinitions;

namespace DataMesh.Demo.ItemProviderSource
{
    public class ItemEditorTypeDefinition : ITypeDefinition
    {
        public string TypeKey => "item";
        public IDictionary<string, ITypeDefinitionItem> Properties => props;

        private readonly Dictionary<string, ITypeDefinitionItem> props = typeof(IItem)
            .GetProperties()
            .Select(prop => prop.Name)
            .ToDictionary(propName => propName,
                propName => (ITypeDefinitionItem)new ItemEditorTypeDefinitionItem(propName));
    }
}