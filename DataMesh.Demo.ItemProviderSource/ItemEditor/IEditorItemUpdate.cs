namespace DataMesh.Demo.ItemProviderSource.ItemEditor
{
    public interface IEditorItemUpdate
    {
        string ItemResourceId { get; }
        string Name { get; }
        decimal? Price { get; }
        string Description { get; }
        bool? Authorized { get; }
        string LastModifiedBy { get; }
    }
}