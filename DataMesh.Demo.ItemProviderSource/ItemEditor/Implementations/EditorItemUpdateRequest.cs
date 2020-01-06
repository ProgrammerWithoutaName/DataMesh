namespace DataMesh.Demo.ItemProviderSource.ItemEditor.Implementations
{
    public class EditorItemUpdateRequest : IEditorItemUpdate
    {
        public string ItemResourceId { get; set; }
        public string Name { get; set; }
        public decimal? Price { get; set; }
        public string Description { get; set; }
        public bool? Authorized { get; set; }
        public string LastModifiedBy { get; set; }
    }
}