using System.Threading.Tasks;
using DataMesh.Composites.MongoDb.Implementations;
using DataMesh.TypeDefinitions;
using Xunit;

namespace DataMesh.Composites.MongoDb.Tests
{

    public class MongoDbTypeRegistry_UnitTests
    {
        [Fact]
        public async Task Registry_ShouldUseMongoToImplementITypeRegistry()
        {
            var fakeStore = new FakeStore<ITypeDefinition>();
            var registry = new MongoDbTypeRegistry(fakeStore);


            var typeRegistryTest = new TypeRegistry_Tests();
            await typeRegistryTest.AssertTypeRegistryImplementation(registry);
        }
    }
}
