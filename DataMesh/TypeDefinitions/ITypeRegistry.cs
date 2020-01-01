using System.Threading.Tasks;

namespace DataMesh.TypeDefinitions
{
    public interface ITypeRegistry
    {
        Task<ITypeDefinition> GetDefinition(string typeKey);
        Task SetDefinition(ITypeDefinition typeDefinition);
    }
}