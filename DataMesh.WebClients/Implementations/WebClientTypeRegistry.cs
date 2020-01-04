using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using DataMesh.TypeDefinitions;

namespace DataMesh.WebClients.Tests
{
    public class WebClientTypeRegistry : IWebClientTypeRegistry
    {
        private readonly IDataMeshClientFactory ClientFactory;
        private readonly TypeRegistryWebServiceSettings RegistryServiceSettings;
        private readonly ITypeDefinitionResponseDeserializer Deserializer;

        public WebClientTypeRegistry(IDataMeshClientFactory clientFactory, 
            TypeRegistryWebServiceSettings registryServiceSettings, 
            ITypeDefinitionResponseDeserializer deserializer)
        {
            ClientFactory = clientFactory;
            RegistryServiceSettings = registryServiceSettings;
            Deserializer = deserializer;
        }

        public async Task<IEnumerable<ITypeDefinition>> GetDefinitions(string authToken)
        {
            var client = ClientFactory.CreateClient(RegistryServiceSettings.GetDefinitions, authToken);
            var response = await client.GetAsync("");

            AssertSuccess(response);

            await using var responseStream = await response.Content.ReadAsStreamAsync();
            return await Deserializer.ParseMany(responseStream);
        }

        // Note: this isn't safe.
        // TODO: Fix Security issue.
        public async Task<ITypeDefinition> GetDefinition(string typeKey, string authToken)
        {
            var client = ClientFactory.CreateClient(RegistryServiceSettings.GetDefinition, authToken);
            var response = await client.GetAsync(typeKey);
            
            AssertSuccess(response);

            await using var responseStream = await response.Content.ReadAsStreamAsync();
            return await Deserializer.ParseSingle(responseStream);
        }

        public async Task SetDefinition(ITypeDefinition typeDefinition, string authToken)
        {
            var client = ClientFactory.CreateClient(RegistryServiceSettings.SetDefinition, authToken);
            var json = JsonSerializer.Serialize(typeDefinition);
            var response = await client.PostAsync("", new StringContent(json));

            AssertSuccess(response);
        }

        public void AssertSuccess(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Error on call to {response.Headers.Location}- Http Status {response.StatusCode} : {response.ReasonPhrase}");
            }
        }
    }
}