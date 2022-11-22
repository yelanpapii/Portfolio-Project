using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using NoSqlExample.WebApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NoSqlExample.WebApi.NoSqlRepository
{
    public partial class PostRepository : IPostRepository
    {
        public async Task AddAsync(Post entity)
        {
            await _mongoCollection.InsertOneAsync(entity);
        }

        public async Task DeleteAsync(int id)
        {
            await _mongoCollection.DeleteOneAsync(x => x.id == id);
        }

        public async Task<IEnumerable<Post>> FindAllAsync()
        {
            return await _mongoCollection.AsQueryable().ToListAsync();
        }

        public async Task<Post> FindOneAsync(int id)
        {
            return await _mongoCollection.AsQueryable().FirstOrDefaultAsync(x => x.id == id);
        }

        public async Task UpdateAsync(int id, Post updatedEntity)
        {
            await _mongoCollection.ReplaceOneAsync(x => x.id == id, updatedEntity);
        }
    }
}