using System.Threading.Tasks;
using DataMesh.TypeDefinitions;

namespace DataMesh.Demo.ItemProviderSource.ItemEditor
{
    public interface IItemWebEditorDataMeshAdapter
    {
        string SourceKey { get; }
        string TypeDefinitionKey { get; }
        ITypeDefinition GetTypeDefinition();

        Task<IItem> Retrieve(string itemResourceId, string authToken);
        IRelinquishmentResponse RelinquishOwnership(IRelinquishmentRequest request, string authToken);
        bool HealthCheck();
    }
}