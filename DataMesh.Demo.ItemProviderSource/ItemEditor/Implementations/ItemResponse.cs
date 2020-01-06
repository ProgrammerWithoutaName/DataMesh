namespace DataMesh.Demo.ItemProviderSource.ItemEditor.Implementations
{
    public class ItemResponse : IItem
    {
        public string ItemId { get; set; }
        public string ItemResourceId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}