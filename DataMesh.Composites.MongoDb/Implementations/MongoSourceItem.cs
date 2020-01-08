namespace DataMesh.Composites.MongoDb.Implementations
{
    public class MongoSourceItem : ICompositeSourceItem
    {
        public string Key { get; set; }
        public string ResourceId { get; set; }
        public string SourceKey { get; set; }
    }
}