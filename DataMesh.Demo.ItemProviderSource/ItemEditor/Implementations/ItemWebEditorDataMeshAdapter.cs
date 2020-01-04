using System.Threading.Tasks;
using DataMesh.TypeDefinitions;

namespace DataMesh.Demo.ItemProviderSource
{
    public class ItemWebEditorDataMeshAdapter : IItemWebEditorDataMeshAdapter
    {
        private readonly IItemEditorStore DataStore;

        public string SourceKey => "ItemWebEditor";
        
        private readonly ITypeDefinition ItemType = new ItemEditorTypeDefinition();
        public string TypeDefinitionKey => ItemType.TypeKey;

        public ITypeDefinition GetTypeDefinition() => ItemType;

        public async Task<IItem> Retrieve(string itemResourceId, string authToken)
            => await DataStore.Get(itemResourceId);

        public IRelinquishmentResponse RelinquishOwnership(IRelinquishmentRequest request, string authToken)
            => new ItemEditorRelinquishmentResponse()
            {
                EntityId = request.EntityId,
                PropertyKey = request.PropertyKey,
                OwnershipRelinquished = authToken == "failMe",
                ResponseDescription = authToken == "failMe" ? "Failure Requested in Auth Token" : null
            };

        public bool HealthCheck() => true;
    }
}