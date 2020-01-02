using System.Collections.Generic;
using DataMesh.TypeDefinitions;

namespace DataMesh.Composites.Tests
{

    //TODO
        // Composite Source Registry API- Needs backing components
        // Composite Read Api
        // Composite Write API
        // Type Registry API
        // Source Registry API
        // Composite Events
        // Partial Reads
        // Cascading Partial Reads
        // Token Handling
        // Error Handling Strategies
        // Permission Handling Strategies
        // Ability to enforce the Types (if registering with the incorrect thing, error should be thrown)
        // Ability to Verify data in types, this would be an extension of Either the Composite or the Type Registry. Honestly could be both.
        // The "what's available" View (HATOS style basically.)
        // Relinquish Ownership- What the row is being replaced with API?
        // Config Settings
        // 3 Test API's- basic crud
        // AWS setup
        
        // what do I need for MVP:
        // Need ability to write, blind ownership transfer is acceptable for MVP.
        // Need Ability to Read
        // Above Requires a Type Registry, but don't need to worry about implementing the Type Enforcement or the Type Validation Services
        // Above Requires a Source Registry
        // For Now, we can "log" events, don't need to work them in immediately
        // Configuration
        // For MVP, just run locally. This is a POC and time is short.

        // MVP Requirements:
        // Composite Read API
        //  - Requires
        //      x - ICompositeSource implementation
        //      x - CompositeSource Mongo Database
        //      - DataSourceRegistry API Client. We should start with the microservice approach out of the gate.
        //      - a few Test APIs- nothing special, just a basic CRUD Api
        // Composite Write API
        //  - Requires
        //      x - Composite Datasource Mongo Database
        //      - Composite Datasource Mongo Client
        //      - ICompositeSink ??- The RelinquishOwnership Api
        //      - DataSourceRegistry API Client(and API)
        //      - a few Test API's that can implement RelinquishOwnership
        // X TypeRegistry API (No type checking, no security atm.)
        //  - Requires
        //      x - TypeRegistry Mongo DB
        //      x - ITypeRegistry Mongo Implementation
        // DataSourceRegistry API
        //  - Requires
        //      x - DataSourceRegistry Mongo Database
        //      x - IDataSourceRegistry Mongo Implementation
        //      - ITypeRegistry API Client
        // DependencyInjection
        // Preferably a nice UI to show stuff (Probably actually required for an MVP)
        // Configuration
        // Test APIs
        //      - Address Service
        //      - Name Service
        //      - Alternate Address Service
        
        
        
        //Done
        
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
