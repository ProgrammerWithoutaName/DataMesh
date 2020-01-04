using System;
using System.Net.Http;

namespace DataMesh.WebClients.Tests
{
    public interface IDataMeshClientFactory
    {
        HttpClient CreateClient(Uri baseSource, string authToken = null);
    }
}