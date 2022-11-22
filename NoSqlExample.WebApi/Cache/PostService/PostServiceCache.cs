using Microsoft.Extensions.Caching.Memory;
using NoSqlExample.WebApi.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NoSqlExample.WebApi.Cache.PostService
{
    public class PostServiceCache : IPostService
    {
        private readonly IMemoryCache _cache;
        private readonly IPostService _postService;

        public PostServiceCache(IMemoryCache cache,
            IPostService postService)
        {
            _cache = cache;
            _postService = postService;
        }

        public Task<IEnumerable<PostDTO>> GetAllPostAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<PostDTO>> GetAllPostByUserIdAsync(int userId)
        {
            throw new NotImplementedException();
        }

        public async Task<PostDTO> GetPostCachedByIdAsync(int id)
        {
            return await _cache.GetOrCreateAsync(id.ToString(), async entry =>
            {
                entry.SlidingExpiration = TimeSpan.FromSeconds(30);
                entry.AbsoluteExpiration = DateTime.Now.AddMinutes(1);
                return await _postService.GetSinglePostByIdAsync(id);
            });
        }

        public Task<PostDTO> GetSinglePostByIdAsync(int postId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<PostDTO>> GetPostsCachedByUserIdAsync(int id)
        {
            return await _cache.GetOrCreateAsync(id.ToString(), async entry =>
            {
                entry.SlidingExpiration = TimeSpan.FromSeconds(30);
                entry.AbsoluteExpiration = DateTime.Now.AddMinutes(1);
                return await _postService.GetAllPostByUserIdAsync(id);
            });
        }
    }
}