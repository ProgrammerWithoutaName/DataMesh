using System;

namespace DataMesh
{
    public interface ITypeSource
    {
        string SourceKey { get; }
        string TypeDefinitionKey { get; }

        Uri HealthCheck { get; }
        Uri Retrieve { get; }
        Uri RelinquishOwnership { get; }
        Uri TypeDefinition { get; }

        bool PartialDataProvider { get; }
    }

    
}
