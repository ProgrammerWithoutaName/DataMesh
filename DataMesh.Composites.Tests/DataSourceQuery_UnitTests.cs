using System.Threading.Tasks;
using Moq;
using Xunit;

namespace DataMesh.Composites.Tests
{
    public class DataSourceQuery_UnitTests
    {
        [Fact]
        public async Task GetResourceFromDataSource_ShouldReturnDatasourceResults()
        {
            var mockRegistry = new Mock<IDataSourceRegistry>();
            var mockSourceClient = new Mock<IDataSourceClient>();
            var mockSource = new Mock<IDataSource>();

            var givenToken = "Given Authentication Token";
            var givenResourceId = "GivenResourceId";
            var givenSourceKey = "givenSourceKey";

            var expectedResults = "Some JSON stuff";
            mockRegistry.Setup(reg => reg.GetSource(givenSourceKey))
                .ReturnsAsync(mockSource.Object);

            mockSourceClient.Setup(client => client.Retrieve(mockSource.Object, givenToken, givenResourceId))
                .ReturnsAsync(expectedResults);

            var DataSourceQuery = new DataSourceQuery(mockRegistry.Object, mockSourceClient.Object);
            var results = await DataSourceQuery.GetResourceFromDataSource(givenToken, givenResourceId, givenSourceKey);

            Assert.Equal(expectedResults, results);
        }
    }
}