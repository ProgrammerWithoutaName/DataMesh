using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataMesh.TypeDefinitions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DataMesh.Demo.ItemProviderSource.Controllers
{
    [ApiController]
    [Route("Editor/[controller]")]
    public class ItemsController
    {
        // Note: Don't do this on production code. Make an actual service layer and keep this thin and clean
        private readonly IItemEditorStore Store;

        public ItemsController(IItemEditorStore store)
        {
            Store = store;
        }

        // We need standard Crud plus one more get by items.
        [HttpGet("{itemResourceId}")]
        public async Task<EditorItem> GetSingle([FromRoute] string itemResourceId)
        {
            var results = await Store.Get(itemResourceId);
            return Map(results);
        }

        [HttpGet("versionsFor/{itemId}")]
        public async Task<IEnumerable<EditorItem>> GetAllVersionsForItemId([FromRoute] string itemId)
        {
            var results = await Store.GetAllVersions(itemId);
            return results.Select(Map);
        }

        [HttpGet("{itemResourceId}/versions")]
        public async Task<IEnumerable<EditorItem>> GetAllVersionsForResource([FromRoute] string itemResourceId)
        {
            var item = await Store.Get(itemResourceId);
            var results = await Store.GetAllVersions(item.ItemId);
            return results.Select(Map);
        }

        [HttpPost]
        public async Task<EditorItem> InsertItem([FromBody] EditorItem item)
        {
            var inserted = await Store.Insert(item);
            return Map(inserted);
        }

        [HttpPut]
        public async Task UpdateItem([FromBody] EditorItemUpdateRequest item)
            => await Store.UpdateItem(item);

        public EditorItem Map(IEditorItem item)
            => new EditorItem()
            {
                ItemId = item.ItemId,
                ItemResourceId = item.ItemResourceId,
                Name = item.Name,
                Description = item.Description,
                Price = item.Price,
                Authorized = item.Authorized,
                InsertedOn = item.InsertedOn,
                LastModifiedBy = item.LastModifiedBy
            };
    }

    public interface IEditorItemUpdate
    {
        string ItemResourceId { get; }
        string Name { get; }
        decimal? Price { get; }
        string Description { get; }
        bool? Authorized { get; }
        string LastModifiedBy { get; }
    }
    public class EditorItemUpdateRequest : IEditorItemUpdate
    {
        public string ItemResourceId { get; set; }
        public string Name { get; set; }
        public decimal? Price { get; set; }
        public string Description { get; set; }
        public bool? Authorized { get; set; }
        public string LastModifiedBy { get; set; }
    }

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

    public class ItemResponse : IItem
    {
        public string ItemId { get; set; }
        public string ItemResourceId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }

    public class TypeDefinitionResponse
    {
        public string TypeKey { get; set; }
        public Dictionary<string, TypeDefinitionResponseItem> Properties { get; set; }
    }

    public class TypeDefinitionResponseItem : ITypeDefinitionItem
    {
        public string TypeKey { get; set; }
        public bool Nullable { get; set; }
        public bool Optional { get; set; }
        public bool Array { get; set; }
    }

    public class ItemEditorOwnershipRelinquishmentRequest : IRelinquishmentRequest
    {
        public string EntityId { get; set; }
        public string PropertyKey { get; set; }
        public string ResourceId { get; set; }
    }
}
