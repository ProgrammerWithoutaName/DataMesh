using System.Collections.Generic;

namespace DataMesh.Composites
{
    public interface ICompositeSourceItem
    {
        string Key { get; }
        string ResourceId { get; }
        string SourceKey { get; }
    }
}