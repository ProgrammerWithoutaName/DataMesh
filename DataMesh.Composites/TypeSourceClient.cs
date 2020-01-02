using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using DataMesh.TypeDefinitions;

namespace DataMesh.Composites
{
    public class TypeSourceClient : ITypeSourceClient
    {
        public async Task<bool> HealthCheck(ITypeSource source)
        {
            var client = CreateClient(source.HealthCheck);
            var results = await client.GetAsync("");
            return results.IsSuccessStatusCode;
        }

        public async Task<string> Retrieve(ITypeSource source, string authToken, string resourceId)
        {
            var client = CreateClient(source.Retrieve, authToken);

            // TODO: Error Handling!!
            var results = await client.GetAsync(resourceId);
            return await results.Content.ReadAsStringAsync();
        }

        public async Task<bool> RelinquishOwnership(ITypeSource source, string authToken, string entityId, string propertyKey, string resourceId)
        {
            var client = CreateClient(source.RelinquishOwnership, authToken);

            // TODO: Error Handling!! Also, figure out how the content is filled out. This is a place holder.
            var results = await client.PostAsync($"{entityId}/{propertyKey}/{resourceId}", new StringContent(""));
            return results.IsSuccessStatusCode;
        }

        public async Task<ITypeDefinition> GetTypeDefinition(ITypeSource source, string authToken)
        {
            var client = CreateClient(source.TypeDefinition, authToken);

            // TODO: Error Handling!!
            var results = await client.GetAsync("");
            await using var responseStream = await results.Content.ReadAsStreamAsync();

            // TODO: Move this, and probably make it better. Also, need tests- outside of TDD.
            var jsonOptions = new JsonSerializerOptions();
            jsonOptions.Converters.Add(new AbstractConverter<Dictionary<string, ITypeDefinitionItem>, 
                IDictionary<string, ITypeDefinitionItem>>());
            jsonOptions.Converters.Add(new AbstractConverter<JsonTypeDefinitionItem, ITypeDefinitionItem>());

            return await JsonSerializer.DeserializeAsync
                <JsonTypeDefinition>(responseStream, jsonOptions);
        }

        public HttpClient CreateClient(Uri baseSource, string authToken = null)
        {
            var client = new HttpClient();
            client.BaseAddress = baseSource;
            if (authToken != null)
            {
                // TODO: Fix this! Not the correct Value!!
                client.DefaultRequestHeaders.Add("AuthorizationToken", authToken);
            }
            return client;
        }

        public class AbstractConverter<TReal, TAbstract> : JsonConverter<TAbstract> where TReal : TAbstract
        {
            public override TAbstract Read(
                ref Utf8JsonReader reader,
                Type typeToConvert,
                JsonSerializerOptions options) => JsonSerializer.Deserialize<TReal>(ref reader, options);
            // Not Cool, breaking Liskov. Fix this?
            public override void Write(Utf8JsonWriter writer, TAbstract value, JsonSerializerOptions options)
            {
                throw new NotImplementedException();
            }
        }
    }
}