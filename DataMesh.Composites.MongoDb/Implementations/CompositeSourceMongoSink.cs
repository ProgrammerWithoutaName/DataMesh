using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DataMesh.Composites.MongoDb.Implementations
{
    public class CompositeSourceMongoSink : ICompositeSourceMongoSink
    {
        // this does not operate off the simple mongo store.
        private readonly IMongoCollection<MongoSerializableCompositeEntity> Source;
        
        public Task Create(string entityId, string typeDefinition, IEnumerable<ICompositeSourceItem> items)
        {
            var composite = new MongoSerializableCompositeEntity()
            {
                Id = new ObjectId().ToString(),
                Items = items.ToDictionary(item => item.Key, MapToItem),
                ResourceId = entityId,
                TypeDefinition = typeDefinition
            };
            return Source.InsertOneAsync(composite);
        }

        public Task Update(string entityId, ICompositeSourceItem updatedItem)
        {
            var update = new UpdateDefinitionBuilder<MongoSerializableCompositeEntity>()
                .Set(composite => composite.Items[updatedItem.Key], MapToItem(updatedItem));

            return Source.UpdateOneAsync(composite => composite.ResourceId == entityId, update);
        }

        public MongoSourceItem MapToItem(ICompositeSourceItem item) =>
            new MongoSourceItem() 
            {
                Key = item.Key,
                ResourceId = item.ResourceId,
                SourceKey = item.SourceKey
            };
    }
}