using System.Threading.Tasks;
using DataMesh.Composites.Tests;
using Xunit;

namespace DataMesh.Composites.MongoDb.Tests
{
    public class MongoDataSourceRegistry_UnitTests
    {
        [Fact]
        public async Task Registry_ShouldImplementCompositeSourceRegistry()
        {
            var fakeStore = new FakeStore<ITypeSource>();
            var registry = new MongoDataSourceRegistry(fakeStore);

            var sourceRegistryTest = new DataSourceRegistry_Tests();

            await sourceRegistryTest.VerifyImplementation(registry);
        }
    }
}