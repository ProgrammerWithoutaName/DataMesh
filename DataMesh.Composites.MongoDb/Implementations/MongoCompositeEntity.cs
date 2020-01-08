using System.Collections.Generic;

namespace DataMesh.Composites.MongoDb.Implementations
{
    public class MongoCompositeEntity : ICompositeEntity
    {
        public string ResourceId { get; set; }
        public string TypeDefinition { get; set; }
        public IEnumerable<ICompositeSourceItem> Items { get; set; }
    }
}