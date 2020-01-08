using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataMesh.Composites.MongoDb.Implementations
{
    public interface ICompositeSourceSink
    {
        // this nessesitates a builder service. But, then again- that builder service is always needed to begin with.
        Task Create(string entityId, string typeDefinition, IEnumerable<ICompositeSourceItem> items);

        Task Update(string entityId, ICompositeSourceItem updatedItem);
    }
}