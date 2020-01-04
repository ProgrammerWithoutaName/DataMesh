using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataMesh
{
    public interface IDataSourceRegistry
    {
        Task<IEnumerable<IDataSource>> GetAllSources();
        Task<IDataSource> GetSource(string sourceKey);
        Task RegisterSource(IDataSource dataSource);
    }
}