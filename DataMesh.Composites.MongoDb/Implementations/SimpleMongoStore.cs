using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace DataMesh.Composites.MongoDb.Implementations
{
    public class SimpleMongoStore<T> : ISimpleMongoStore<T>
    {
        private readonly IMongoCollection<T> Store;

        public SimpleMongoStore(IMongoCollection<T> store)
        {
            Store = store;
        }

        public async Task<T> GetFirst(Expression<Func<T, bool>> filter)
        {
            var cursor = await Store.FindAsync(filter);
            return await cursor.FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>> filter)
        {
            var cursor = await Store.FindAsync(filter);
            return await cursor.ToListAsync();
        }

        public Task Set(Expression<Func<T, bool>> filter, T item)
            => Store.ReplaceOneAsync(filter, item);
    }
}