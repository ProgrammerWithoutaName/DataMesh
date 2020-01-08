using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using DataMesh.WebClients.Tests;

namespace DataMesh.Composites
{
    public class WebClientDataSourceRegistry : IWebClientDataSourceRegistry
    {

        private readonly IDataMeshClientFactory ClientFactory;
        private readonly DataSourceRegistrySettings registrySettings;

        public async Task<IEnumerable<IDataSource>> GetAllSources()
        {
            var client = CreateClient();
            var results = await client.GetAsync("sources");
            await using var responseStream = await results.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<JsonDataSource[]>(responseStream);
        }

        public async Task<IDataSource> GetSource(string sourceKey)
        {
            var client = CreateClient();
            //TODO: Make this Secure!!!!!!
            var results = await client.GetAsync($"sources/{sourceKey}");
            await using var responseStream = await results.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<JsonDataSource>(responseStream);
        }

        public async Task RegisterSource(IDataSource dataSource)
        {
            var client = CreateClient();
            await client.PostAsync("sources/register", new StringContent(JsonSerializer.Serialize(dataSource)));
        }

        public HttpClient CreateClient()
            => ClientFactory.CreateClient(registrySettings.DataSourceRegistryBaseUri);
    }
}