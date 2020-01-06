using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DataMesh.Demo.ItemProviderSource.ItemEditor.Implementations
{
    public class MongoItemEditorStore : IItemEditorStore
    {
        private readonly IMongoCollection<EditorItem> ItemEditorStore;

        public MongoItemEditorStore(IMongoCollection<EditorItem> itemEditorStore)
        {
            ItemEditorStore = itemEditorStore;
        }

        public async Task<IEditorItem> Get(string itemResourceId)
        {
            var query = await ItemEditorStore.FindAsync(item => item.ItemResourceId == itemResourceId);
            return await query.FirstAsync();
        }
                
        public async Task<IEditorItem> Insert(IEditorItem item)
        {
            var newItem = new EditorItem()
            {
                ItemResourceId = new ObjectId().ToString(),
                ItemId = item.ItemId,
                Description = item.Description,
                Name = item.Name,
                Price = item.Price,
                Authorized = item.Authorized,
                LastModifiedBy = item.LastModifiedBy,
                InsertedOn = DateTime.UtcNow
            };
            await ItemEditorStore.InsertOneAsync(newItem);
            return newItem;
        }

        public async Task<IEnumerable<IEditorItem>> GetAllVersions(string itemId)
        {
            var itemsQuery = await ItemEditorStore.FindAsync(item => item.ItemId == itemId);
            return await itemsQuery.ToListAsync();
        }

        public async Task UpdateItem(IEditorItemUpdate item)
        {
            var updateCommand = CreateUpdateCommand(item);
            await ItemEditorStore.UpdateOneAsync(existingItem => existingItem.ItemResourceId == item.ItemResourceId,
                updateCommand);
        }

        public UpdateDefinition<EditorItem> CreateUpdateCommand(IEditorItemUpdate item)
        {
            var updates = new List<Func<UpdateDefinition<EditorItem>, UpdateDefinition<EditorItem>>>();
            if (item.Name != null)
                updates.Add(updatedValues => updatedValues.Set(existing => existing.Name, item.Name));
            if (item.Description != null)
                updates.Add(updatedValues => updatedValues.Set(existing => existing.Description, item.Description));
            if (item.Price != null)
                updates.Add(updatedValues => updatedValues.Set(existing => existing.Price, item.Price));
            if (item.Authorized != null)
                updates.Add(updatedValues => updatedValues.Set(existing => existing.Authorized, item.Authorized));

            var updateCommand = Builders<EditorItem>.Update.Set(existing => existing.LastModifiedBy, item.LastModifiedBy);
            updates.ForEach(update => updateCommand = update(updateCommand));

            return updateCommand;
        }

    }
}