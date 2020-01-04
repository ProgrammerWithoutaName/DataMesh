using System;

namespace DataMesh.Demo.ItemProviderSource
{
    public interface IItem
    {
        string ItemId { get; }
        string ItemResourceId { get; }
        string Name { get; }
        string Description { get; }
        decimal Price { get; }
    }

    public interface IEditorItem : IItem
    {
        string LastModifiedBy { get; }
        bool Authorized { get; }
        DateTime InsertedOn { get; }
    }

}