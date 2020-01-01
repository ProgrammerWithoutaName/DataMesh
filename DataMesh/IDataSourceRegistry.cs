using System.Threading.Tasks;

namespace DataMesh
{
    public interface IDataSourceRegistry
    {
        Task<ITypeSource> GetSource(string sourceKey);
        Task RegisterSource(ITypeSource typeSource);
    }
}