namespace DataMesh.Demo.ItemProviderSource.ItemEditor.Implementations
{
    public class ItemEditorRelinquishmentResponse : IRelinquishmentResponse
    {
        public string EntityId { get; set; }
        public string PropertyKey { get; set; }
        public bool OwnershipRelinquished { get; set; }
        public string ResponseDescription { get; set; }
    }
}