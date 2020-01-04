using System.Net.Http;
using System.Threading.Tasks;
using DataMesh.TypeDefinitions;
using DataMesh.WebClients.Tests;

namespace DataMesh.Composites
{
    public class DataSourceClient : IDataSourceClient
    {
        private readonly IDataMeshClientFactory ClientFactory;
        private readonly ITypeDefinitionResponseDeserializer TypeDefinitionDeserializer;

        public DataSourceClient(IDataMeshClientFactory clientFactory, ITypeDefinitionResponseDeserializer typeDefinitionDeserializer)
        {
            ClientFactory = clientFactory;
            TypeDefinitionDeserializer = typeDefinitionDeserializer;
        }

        public async Task<bool> HealthCheck(IDataSource source)
        {
            var client = ClientFactory.CreateClient(source.HealthCheck);
            var results = await client.GetAsync("");
            return results.IsSuccessStatusCode;
        }

        public async Task<string> Retrieve(IDataSource source, string authToken, string resourceId)
        {
            var client = ClientFactory.CreateClient(source.Retrieve, authToken);

            // TODO: Error Handling!!
            var results = await client.GetAsync(resourceId);
            return await results.Content.ReadAsStringAsync();
        }

        public async Task<bool> RelinquishOwnership(IDataSource source, string authToken, string entityId, string propertyKey, string resourceId)
        {
            var client = ClientFactory.CreateClient(source.RelinquishOwnership, authToken);

            // TODO: Error Handling!! Also, figure out how the content is filled out. This is a place holder.
            var results = await client.PostAsync($"{entityId}/{propertyKey}/{resourceId}", 
                new StringContent(""));
            return results.IsSuccessStatusCode;
        }

        public async Task<ITypeDefinition> GetTypeDefinition(IDataSource source, string authToken)
        {
            var client = ClientFactory.CreateClient(source.TypeDefinition, authToken);

            // TODO: Error Handling!!
            var results = await client.GetAsync("");
            await using var responseStream = await results.Content.ReadAsStreamAsync();

            return await TypeDefinitionDeserializer.ParseSingle(responseStream);
        }

    }
}