using System.Collections.Generic;
using System.Threading.Tasks;
using DataMesh.Demo.ItemProviderSource.Controllers;

namespace DataMesh.Demo.ItemProviderSource
{
    public interface IItemEditorStore
    {
        Task<IEditorItem> Get(string itemResourceId);
        Task<IEditorItem> Insert(IEditorItem item);
        Task<IEnumerable<IEditorItem>> GetAllVersions(string itemId);
        Task UpdateItem(IEditorItemUpdate item);
    }
}