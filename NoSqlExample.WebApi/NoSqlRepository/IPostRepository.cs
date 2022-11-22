using NoSqlExample.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NoSqlExample.WebApi.NoSqlRepository
{
    public interface IPostRepository
    {
        public Task<IEnumerable<Post>> FindAllAsync();
        public Task<Post> FindOneAsync(int id);
        public Task AddAsync(Post entity);
        public Task UpdateAsync(int id, Post updatedEntity);
        public Task DeleteAsync(int id);
    }
}
