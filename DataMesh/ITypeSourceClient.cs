using System.Threading.Tasks;
using DataMesh.TypeDefinitions;

namespace DataMesh.Composites
{
    public interface ITypeSourceClient
    {
        Task<bool> HealthCheck(ITypeSource source);
        Task<string> Retrieve(ITypeSource source, string authToken, string resourceId);
        // This will need more work, and we will need to provide something for getting what it's going to be changed to.
        Task<bool> RelinquishOwnership(ITypeSource source, string authToken, string entityId, string propertyKey, string resourceId);
        Task<ITypeDefinition> GetTypeDefinition(ITypeSource source, string authToken);
    }
}