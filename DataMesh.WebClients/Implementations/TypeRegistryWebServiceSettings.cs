using System;

namespace DataMesh.WebClients.Tests
{
    public class TypeRegistryWebServiceSettings
    {
        public Uri GetDefinitions { get; set; }
        public Uri GetDefinition { get; set; }
        public Uri SetDefinition { get; set; }
    }

    // Not sure this is actually testable? revisit.
}
