using System.Collections.Generic;
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
            var fakeEntity = new Mock<ICompositeEntity>();
            var store = new FakeStore<ICompositeEntity>();

            var expectedCompositeEntity = new FakeCompositeEntity()
            {
                ResourceId = "GivenResourceId",
                TypeDefinition = "GivenType",
                Items = new[] {new FakeCompositeSourceItem(),}
            };

            store.Store.Add(expectedCompositeEntity);

            var query = new CompositeSourceMongoQuery(store);
            var results = await query.GetComposite(expectedCompositeEntity.ResourceId);

            Assert.Equal(expectedCompositeEntity, results);
        }

        public class FakeCompositeEntity : ICompositeEntity
        {
            public string ResourceId { get; set; }
            public string TypeDefinition { get; set; }
            public IEnumerable<ICompositeSourceItem> Items { get; set; }
        }

        public class FakeCompositeSourceItem : ICompositeSourceItem
        {
            public string Key { get; set; }
            public string ResourceId { get; set; }
            public string SourceKey { get; set; }
        }
    }
}