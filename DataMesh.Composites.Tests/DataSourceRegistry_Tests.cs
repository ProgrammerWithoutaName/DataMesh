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
            var fakeSource = new FakeTypeSource()
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

        public class FakeTypeSource : ITypeSource
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

    

    // Implementation base Exists
    public interface ICompositeSourceRegistryApi
    {
        // this should be able to double for updates.
        void RegisterSource(string authToken, ITypeSource sourceService);
        ITypeSource GetSource(string authToken, string sourceKey);
    }


    // We Need the ability to register DataSources

    // A datasource must provide the following::
    // // Source:
    // Needs API for Ownership Relinquishment that must accept a token
    // Needs API for Health Check
    // Needs API for Data Retrieval that must accept a token and a ResourceID
    // Needs Events for the following: Ownership Relinquished, Property Updated
    // Needs Following APIs: HealthCheck, RelinquishOwnership, Retrieve, CheckType




    public class DataQueryServiceRouter_Tests
    {

    }

    // In Reality, we break it up:

    // This represents the full read, so the base does not actually exist yet.
    // This will require the following: TypeDefinitionService, CompositeSourceService, DataSourceService, and DataQueryService.
    // We currently have a light version of the top, but that may need rewritten
    public interface ICompositeEntityReadApi
    {
        bool HealthCheck();
        ICompositeEntity Get(string authToken, string entityResourceId);
        ICompositeEntity GetPartial(string authToken, string entityResourceId, ISet<string> keys);
    }

    // base does not exist
    // This will require the following: TypeDefinitionService, SourceRegistryService, Technically The Events, CompositeSourceStore
    public interface ICompositeEntityWriteApi
    {
        void PropertyUpdated(string authToken, string entityResourceId, string key, string itemResourceId);
        ITypeDefinition GetTypeDefinition(string authToken, string typeDefinition);
        bool RequestOwnership(string authToken, string entityResourceId, string key, string itemResourceId);
        bool HealthCheck();

        ICompositeEntity CreateEntity(string authToken, ICompositeEntity entity);
    }

    
    // Base Exists for mongo
    public interface ITypeDefinitionRegistryApi
    {
        void RegisterType(ITypeDefinition type);
        IEnumerable<ITypeDefinition> GetTypes(string[] typeDefinitionKeys);
    }

    // don't have events yet.
    public enum CompositeEvents
    {
        CompositeCreated, // Id
        CompositeUpdated, // Id, Keys
        CompositeItemOwnershipRelinquished
    }

}