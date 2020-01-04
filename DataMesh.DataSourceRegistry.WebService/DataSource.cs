using System;

namespace DataMesh.DataSourceRegistry.WebService
{
    public class DataSource : IDataSource
    {
        public string SourceKey { get; set; }
        public string TypeDefinitionKey { get; set; }
        public bool PartialDataProvider { get; set; }
        public Uri HealthCheck { get; set; }
        public Uri Retrieve { get; set; }
        public Uri RelinquishOwnership { get; set; }
        public Uri TypeDefinition { get; set; }
        
    }
}