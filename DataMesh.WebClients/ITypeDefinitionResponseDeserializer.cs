using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using DataMesh.TypeDefinitions;

namespace DataMesh.WebClients.Tests
{
    public interface ITypeDefinitionResponseDeserializer
    {
        Task<ITypeDefinition> ParseSingle(Stream responseStream);
        Task<IEnumerable<ITypeDefinition>> ParseMany(Stream responseStream);
    }
}