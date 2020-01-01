using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DataMesh.Composites.MongoDb.Tests
{
    public class FakeStore<T> : ISimpleMongoStore<T>
    {
        public readonly List<T> Store = new List<T>();

        public Task<T> GetFirst(Expression<Func<T, bool>> filter)
            => Task.FromResult(Store.FirstOrDefault(filter.Compile()));


        public Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>> filter)
            => Task.FromResult(Store.Where(filter.Compile()));

        public Task Set(Expression<Func<T, bool>> filter, T item)
        {
            var compiledFilter = filter.Compile();
            Store.RemoveAll(item => compiledFilter(item));
            Store.Add(item);
            return Task.CompletedTask;
        }
    }
}