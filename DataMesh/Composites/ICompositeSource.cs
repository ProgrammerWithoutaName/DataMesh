using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataMesh.Composites
{
    public interface ICompositeSource
    {
        Task<IDictionary<string, string>> GetAll(string authToken, string resourceId);
        Task<IDictionary<string, string>> Get(string authToken, string resourceId, ISet<string> keys);
    }
}