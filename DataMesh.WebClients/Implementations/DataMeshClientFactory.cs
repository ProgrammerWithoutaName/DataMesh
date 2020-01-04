using System;
using System.Net.Http;

namespace DataMesh.WebClients.Tests
{
    public class DataMeshClientFactory : IDataMeshClientFactory
    {
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
    }
}