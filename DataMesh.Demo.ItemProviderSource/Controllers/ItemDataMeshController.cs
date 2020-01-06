using System.Linq;
using System.Threading.Tasks;
using DataMesh.Demo.ItemProviderSource.ItemEditor;
using DataMesh.Demo.ItemProviderSource.ItemEditor.Implementations;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace DataMesh.Demo.ItemProviderSource.Controllers
{
    [ApiController]
    [Route("datamesh/[controller]")]
    public class ItemDataMeshController : ControllerBase
    {
        private readonly IItemWebEditorDataMeshAdapter DataMeshAdapter;

        public ItemDataMeshController(IItemWebEditorDataMeshAdapter dataMeshAdapter)
        {
            DataMeshAdapter = dataMeshAdapter;
        }


        [HttpGet("{resourceId}")]
        public async Task<ItemResponse> Get([FromRoute] string itemResourceId, [FromHeader] AuthenticationToken authToken)
        {
            var item = await DataMeshAdapter.Retrieve(itemResourceId, authToken.Value);
            return new ItemResponse()
            {
                Description = item.Description,
                ItemId = item.ItemId,
                ItemResourceId = item.ItemResourceId,
                Name = item.Name,
                Price = item.Price
            };
        }

        [HttpGet("healthCheck")]
        public bool HealthCheck() => DataMeshAdapter.HealthCheck();

        [HttpGet("typeDefinition")]
        public TypeDefinitionResponse GetTypeDefinition()
        {
            var definition = DataMeshAdapter.GetTypeDefinition();
            return new TypeDefinitionResponse()
            {
                TypeKey = definition.TypeKey,
                Properties = definition.Properties.ToDictionary(
                    kv => kv.Key,
                    kv => new TypeDefinitionResponseItem()
                    {
                        TypeKey = kv.Value.TypeKey,
                        Array = kv.Value.Array,
                        Nullable = kv.Value.Nullable,
                        Optional = kv.Value.Optional
                    })
            };
        }

        [HttpPost("Relinquish")]
        public ItemEditorRelinquishmentResponse RelinquishOwnership(
            [FromBody] ItemEditorOwnershipRelinquishmentRequest request,
            [FromHeader] AuthenticationToken authToken)
        {
            var response = DataMeshAdapter.RelinquishOwnership(request, authToken.Value);
            return new ItemEditorRelinquishmentResponse()
            {
                EntityId = response.EntityId,
                PropertyKey = response.PropertyKey,
                OwnershipRelinquished = response.OwnershipRelinquished,
                ResponseDescription = response.ResponseDescription
            };
        }
    }
}