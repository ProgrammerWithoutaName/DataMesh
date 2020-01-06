using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataMesh.Demo.ItemProviderSource.ItemEditor
{
    public interface IItemEditorStore
    {
        Task<IEditorItem> Get(string itemResourceId);
        Task<IEditorItem> Insert(IEditorItem item);
        Task<IEnumerable<IEditorItem>> GetAllVersions(string itemId);
        Task UpdateItem(IEditorItemUpdate item);
    }
}