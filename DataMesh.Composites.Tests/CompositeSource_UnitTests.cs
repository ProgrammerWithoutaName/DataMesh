using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using Xunit;

namespace DataMesh.Composites.Tests
{
    public class CompositeSource_UnitTests
    {

        public class FakeCompositeSourceItem : ICompositeSourceItem
        {
            public string Key { get; set; }

            public string ResourceId { get; set; }

            public string SourceKey { get; set; }
        }

        public class FakeCompositeEntity : ICompositeEntity
        {
            public string ResourceId { get; set; }
            public string TypeDefinition { get; set; }
            public IEnumerable<ICompositeSourceItem> Items { get; set; }
        }

        public class CompositeSourceScaffold
        {
            public MockRepository MockRepo;
            public Mock<ICompositeSourceQuery> MockCompositeQuery;
            public Mock<IDataSourceQuery> MockDataQuery;

            public ICompositeSource CompositeSource;

            public string FakeResourceId = "compositeResourceId";
            public string FakeAuthToken = "SomeAuthToken";
            public IEnumerable<ICompositeSourceItem> FakeSourceItems;
            public ICompositeEntity FakeEntity;

            public CompositeSourceScaffold()
            {
                MockRepo = new MockRepository(MockBehavior.Default);
                MockDataQuery = MockRepo.Create<IDataSourceQuery>();
                MockCompositeQuery = MockRepo.Create<ICompositeSourceQuery>();

                FakeSourceItems = new []
                {
                    new FakeCompositeSourceItem { Key = "A", ResourceId = "A1", SourceKey = "SourceA" },
                    new FakeCompositeSourceItem { Key = "B", ResourceId = "B1", SourceKey = "SourceB" },
                    new FakeCompositeSourceItem { Key = "C", ResourceId = "C1", SourceKey = "SourceC" }
                };

                FakeEntity = new FakeCompositeEntity()
                {
                    ResourceId = FakeResourceId,
                    Items = FakeSourceItems,
                    TypeDefinition = "FakeSourceEntity"
                };

                foreach (var item in FakeSourceItems)
                {
                    MockDataQuery.Setup(query => query.GetResourceFromDataSource(FakeAuthToken, item.ResourceId, item.SourceKey))
                        .Returns(Task.FromResult(GetDatasourcesValue(item)));
                }

                MockCompositeQuery.Setup(query => query.GetComposite(FakeResourceId)).Returns(Task.FromResult(FakeEntity));
                CompositeSource = new CompositeSource(MockCompositeQuery.Object, MockDataQuery.Object);
            }

            public string GetDatasourcesValue(ICompositeSourceItem item) => item.Key + item.ResourceId;
        }

        [Fact]
        public async Task GetAll_ShouldReturnDataFromAllSources()
        {
            var test = new CompositeSourceScaffold();
            var expectedResults = test.FakeSourceItems.Select(item => 
                new KeyValuePair<string,string>(item.Key, test.GetDatasourcesValue(item)));

            var results = await test.CompositeSource.GetAll(test.FakeAuthToken, test.FakeResourceId);

            Assert.Equal(expectedResults.Count(), results.Count);
            Assert.All(expectedResults, item => Assert.Equal(item.Value, results[item.Key]));
        }

        [Fact]
        public async Task Get_ShouldOnlyRetrieveValuesRequested()
        {
            var test = new CompositeSourceScaffold();
            
            var givenKeys = new HashSet<string>() { "A", "B" };

            var expectedResults = test.FakeSourceItems
                .Where(item => givenKeys.Contains(item.Key))
                .Select(item =>
                    new KeyValuePair<string, string>(item.Key, test.GetDatasourcesValue(item)));

            var results = await test.CompositeSource.Get(test.FakeAuthToken, test.FakeResourceId, givenKeys);

            Assert.Equal(expectedResults.Count(), results.Count);
            Assert.All(expectedResults, item => Assert.Equal(item.Value, results[item.Key]));
        }
    }
}