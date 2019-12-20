using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataMesh.Composites
{

    public interface ICompositeSourceItem
    {
        string Key { get; }
        string ResourceId { get; }
        string SourceKey { get; }
    }

    public interface IDataSourceQuery
    {
        Task<string> GetResourceFromDataSource(string resourceId, string sourceKey);
    }

    public interface ICompositeSourceQuery
    {
        Task<IEnumerable<ICompositeSourceItem>> GetComposite(string resourceId);
    }

    public class CompositeSource : ICompositeSource
    {
        private readonly ICompositeSourceQuery compositeQuery;
        private readonly IDataSourceQuery dataQuery;
        public CompositeSource(ICompositeSourceQuery compositeQuery, IDataSourceQuery dataQuery)
        {
            this.compositeQuery = compositeQuery;
            this.dataQuery = dataQuery;
        }

        public async Task<IDictionary<string, string>> GetAll(string resourceId)
        {
            var composite = await compositeQuery.GetComposite(resourceId);

            return await Get(composite);
        }

        public async Task<IDictionary<string, string>> Get(string resourceId, ISet<string> keys)
        {
            var compositeItems = await compositeQuery.GetComposite(resourceId);
            return await Get(compositeItems.Where(item => keys.Contains(item.Key)));
        }

        public async Task<IDictionary<string, string>> Get(IEnumerable<ICompositeSourceItem> sourceItems)
        {
            return sourceItems.AsParallel()
                .Select(compositeItem =>
                    new KeyValuePair<string, string>(compositeItem.Key,
                        dataQuery.GetResourceFromDataSource(compositeItem.ResourceId,
                        compositeItem.SourceKey).Result))
                .ToDictionary(kv => kv.Key, kv => kv.Value); // there's got to be a better way to do this.
        }
    }

    public interface ICompositeSourceResolvedItem
    {
        string Key { get; }
        string JsonValue { get; }
    }

    public interface ICompositeSource
    {
        Task<IDictionary<string, string>> GetAll(string resourceId);
        Task<IDictionary<string, string>> Get(string resourceId, ISet<string> keys);
    }
}
