using System.Collections.Generic;

namespace DataMesh.Composites
{
    public interface ICompositeEntity
    {
        public string ResourceId { get; }
        public string TypeDefinition { get; }
        IEnumerable<ICompositeSourceItem> Items { get; }
    }
}