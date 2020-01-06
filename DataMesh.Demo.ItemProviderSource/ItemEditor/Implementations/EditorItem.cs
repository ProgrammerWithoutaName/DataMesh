using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DataMesh.Demo.ItemProviderSource.ItemEditor.Implementations
{
    public class EditorItem : IEditorItem
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("Id")]
        public string ItemResourceId { get; set; }
        public string ItemId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string LastModifiedBy { get; set; }
        public bool Authorized { get; set; }
        public DateTime InsertedOn { get; set; }
    }
}