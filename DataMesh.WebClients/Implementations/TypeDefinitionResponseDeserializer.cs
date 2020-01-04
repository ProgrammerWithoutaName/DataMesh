using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using DataMesh.Composites;
using DataMesh.TypeDefinitions;

namespace DataMesh.WebClients.Tests
{
    public class TypeDefinitionResponseDeserializer : ITypeDefinitionResponseDeserializer
    {
        public async Task<ITypeDefinition> ParseSingle(Stream responseStream)
            => await JsonSerializer.DeserializeAsync<JsonTypeDefinition>(responseStream, ConfigureSerializer());

        public async Task<IEnumerable<ITypeDefinition>> ParseMany(Stream responseStream)
            => await JsonSerializer.DeserializeAsync<JsonTypeDefinition[]>(responseStream, ConfigureSerializer());

        public JsonSerializerOptions ConfigureSerializer()
        {
            var options = new JsonSerializerOptions();

            options.Converters.Add(new AbstractConverter<Dictionary<string, ITypeDefinitionItem>,
                IDictionary<string, ITypeDefinitionItem>>());
            options.Converters.Add(new AbstractConverter<JsonTypeDefinitionItem, ITypeDefinitionItem>());

            return options;
        }
    }
}