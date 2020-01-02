using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataMesh.TypeDefinitions
{
    public interface ITypeRegistry
    {
        Task<IEnumerable<ITypeDefinition>> GetDefinitions();
        Task<ITypeDefinition> GetDefinition(string typeKey);
        Task SetDefinition(ITypeDefinition typeDefinition);
    }
}