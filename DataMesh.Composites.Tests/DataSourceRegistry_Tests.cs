using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataMesh.TypeDefinitions;
using Xunit;

namespace DataMesh.Composites.Tests
{
    public class DataSourceRegistry_Tests
    {
        public async Task VerifyImplementation(IDataSourceRegistry registry)
        {
            var fakeSource = new FakeDataSource()
            {
                SourceKey = "GivenSourceKey",
                TypeDefinitionKey = "GivenTypeDefinitionKey",
                PartialDataProvider = true,
                Retrieve = new Uri("http://GivenRetrieveUri.com"),
                RelinquishOwnership = new Uri("http://GivenRelinquishUri.com"),
                TypeDefinition = new Uri("http://GivenTypeDefinition.com"),
                HealthCheck = new Uri("http://GivenHealthCheckUri.com")
            };

            await registry.RegisterSource(fakeSource);
            var results = await registry.GetSource(fakeSource.SourceKey);
            
            Assert.Equal(fakeSource.SourceKey, results.SourceKey);
            Assert.Equal(fakeSource.TypeDefinitionKey, results.TypeDefinitionKey);
            Assert.Equal(fakeSource.PartialDataProvider, results.PartialDataProvider);
            Assert.Equal(fakeSource.Retrieve.AbsoluteUri, results.Retrieve.AbsoluteUri);
            Assert.Equal(fakeSource.RelinquishOwnership.AbsoluteUri, results.RelinquishOwnership.AbsoluteUri);
            Assert.Equal(fakeSource.TypeDefinition.AbsoluteUri, results.TypeDefinition.AbsoluteUri);
            Assert.Equal(fakeSource.HealthCheck.AbsoluteUri, results.HealthCheck.AbsoluteUri);
        }

        public class FakeDataSource : IDataSource
        {
            
            public string SourceKey { get; set; }
            public string TypeDefinitionKey { get; set; }
            public Uri HealthCheck { get; set; }
            public Uri Retrieve { get; set; }
            public Uri RelinquishOwnership { get; set; }
            public Uri TypeDefinition { get; set; }
            public bool PartialDataProvider { get; set; }
        } 
    }

    


}