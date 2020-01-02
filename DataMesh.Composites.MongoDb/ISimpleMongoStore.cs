using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DataMesh.Composites.MongoDb
{
    public interface ISimpleMongoStore<T>
    {
        Task<T> GetFirst(Expression<Func<T, bool>> filter);
        Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>> filter);
        Task<IEnumerable<T>> GetAll();
        Task Set(Expression<Func<T, bool>> filter, T item);
    }
}