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
            var fakeStore = new FakeStore<MongoSerializableTypeDefinition>();
            var typeDefinitionFactory = new MongoTypeDefinitionFactory();
            var registry = new MongoDbTypeRegistry(fakeStore, typeDefinitionFactory);

            var typeRegistryTest = new TypeRegistry_Tests();
            await typeRegistryTest.AssertTypeRegistryImplementation(registry);
        }
    }
}
