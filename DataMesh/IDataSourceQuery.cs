using System.Threading.Tasks;

namespace DataMesh
{
    public interface IDataSourceQuery
    {
        Task<string> GetResourceFromDataSource(string authToken, string resourceId, string sourceKey);
    }
}