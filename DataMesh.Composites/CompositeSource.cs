using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataMesh.Composites
{
    public class CompositeSource : ICompositeSource
    {
        private readonly ICompositeSourceQuery CompositeQuery;
        private readonly IDataSourceQuery DataQuery;

        public CompositeSource(ICompositeSourceQuery compositeQuery, IDataSourceQuery dataQuery)
        {
            CompositeQuery = compositeQuery;
            DataQuery = dataQuery;
        }

        public async Task<IDictionary<string, string>> GetAll(string authToken, string resourceId)
        {
            var composite = await CompositeQuery.GetComposite(resourceId);

            return await Get(authToken, composite.Items);
        }

        public async Task<IDictionary<string, string>> Get(string authToken, string resourceId, ISet<string> keys)
        {
            var compositeEntity = await CompositeQuery.GetComposite(resourceId);
            return await Get(authToken, compositeEntity.Items.Where(item => keys.Contains(item.Key)));
        }

        public Task<IDictionary<string, string>> Get(string authToken, IEnumerable<ICompositeSourceItem> sourceItems)
        {
            // This is ugly AF, fix it.
            return Task.FromResult((IDictionary<string,string>)
                sourceItems
                    .AsParallel()
                    .Select(compositeItem =>
                        new KeyValuePair<string, string>(compositeItem.Key,
                            DataQuery.GetResourceFromDataSource(authToken, compositeItem.ResourceId,
                            compositeItem.SourceKey).Result))
                    .ToDictionary(kv => kv.Key, kv => kv.Value)); // there's got to be a better way to do this.
        }
    }

}
