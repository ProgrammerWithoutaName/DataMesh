namespace DataMesh.Demo.ItemProviderSource.ItemEditor.Implementations
{
    public class ItemEditorOwnershipRelinquishmentRequest : IRelinquishmentRequest
    {
        public string EntityId { get; set; }
        public string PropertyKey { get; set; }
        public string ResourceId { get; set; }
    }
}