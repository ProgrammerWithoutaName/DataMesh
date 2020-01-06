using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataMesh.Demo.ItemProviderSource.ItemEditor;
using DataMesh.Demo.ItemProviderSource.ItemEditor.Implementations;
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

        private EditorItem Map(IEditorItem item)
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
}
