using System.Threading.Tasks;
using DataMesh.TypeDefinitions;

namespace DataMesh.Composites
{
    public interface IDataSourceClient
    {
        Task<bool> HealthCheck(IDataSource source);
        Task<string> Retrieve(IDataSource source, string authToken, string resourceId);
        // This will need more work, and we will need to provide something for getting what it's going to be changed to.
        Task<bool> RelinquishOwnership(IDataSource source, string authToken, string entityId, string propertyKey, string resourceId);
        Task<ITypeDefinition> GetTypeDefinition(IDataSource source, string authToken);
    }
}