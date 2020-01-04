using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace DataMesh.TypeDefinitions
{
    public interface ITypeRegistry
    {
        Task<IEnumerable<ITypeDefinition>> GetDefinitions(string authToken);
        Task<ITypeDefinition> GetDefinition(string typeKey, string authToken);
        Task SetDefinition(ITypeDefinition typeDefinition, string authToken);
    }
}