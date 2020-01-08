using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataMesh.Composites.MongoDb.Implementations;
using Moq;
using Xunit;

namespace DataMesh.Composites.MongoDb.Tests
{
    public class CompositeSourceMongoQuery_UnitTests
    {
        [Fact]
        public async Task GetComposite_ShouldReturnGivenComposite()
        {
            var fakeEntity = new Mock<MongoSerializableCompositeEntity>();
            var store = new FakeStore<MongoSerializableCompositeEntity>();

            var givenCompositeEntity = new MongoSerializableCompositeEntity()
            {
                ResourceId = "GivenResourceId",
                TypeDefinition = "GivenType",
                Items = new Dictionary<string, MongoSourceItem>(){ {"thing", new MongoSourceItem() { SourceKey = "place", Key = "thing", ResourceId = "thingId"}} }
            };

            store.Store.Add(givenCompositeEntity);

            var query = new CompositeSourceMongoQuery(store);
            var results = await query.GetComposite(givenCompositeEntity.ResourceId);

            Assert.Equal(givenCompositeEntity.ResourceId, results.ResourceId);
            Assert.Equal(givenCompositeEntity.TypeDefinition, results.TypeDefinition);
            Assert.Equal(givenCompositeEntity.Items.Count, results.Items.Count());

            foreach (var item in givenCompositeEntity.Items.Values)
            {
                var comparison = results.Items.First(resultItem => item.Key == resultItem.Key);
                Assert.Equal(item.SourceKey, comparison.SourceKey);
                Assert.Equal(item.SourceKey, comparison.SourceKey);
            }
        }
    }
}