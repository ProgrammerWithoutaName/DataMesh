namespace DataMesh.Demo.ItemProviderSource
{
    public class ItemEditorRelinquishmentResponse : IRelinquishmentResponse
    {
        public string EntityId { get; set; }
        public string PropertyKey { get; set; }
        public bool OwnershipRelinquished { get; set; }
        public string ResponseDescription { get; set; }
    }
}