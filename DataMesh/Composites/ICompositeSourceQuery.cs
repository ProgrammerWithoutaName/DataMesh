using System.Threading.Tasks;

namespace DataMesh.Composites
{
    public interface ICompositeSourceQuery
    {
        Task<ICompositeEntity> GetComposite(string resourceId);
    }
}