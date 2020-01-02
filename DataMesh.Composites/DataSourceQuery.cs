using System.Threading.Tasks;

namespace DataMesh.Composites
{
    public class DataSourceQuery : IDataSourceQuery
    {
        private readonly IDataSourceRegistry Registry;
        private readonly ITypeSourceClient SourceClient;

        public DataSourceQuery(IDataSourceRegistry registry, ITypeSourceClient sourceClient)
        {
            Registry = registry;
            SourceClient = sourceClient;
        }

        //private readonly HttpClient HttpClientFactory;
        // we need to make a web request. I will need to put in something for adding a key, or passing it somehow...
        // Hey, actually, so for ability to access this, this is actually what Steven was trying to do. He was afraid to delegate.
        // Note: this is actually testable!!
        public async Task<string> GetResourceFromDataSource(string authToken, string resourceId, string sourceKey)
        {
            var source = await Registry.GetSource(sourceKey);
            return await SourceClient.Retrieve(source, authToken, resourceId);
        }
    }
}
