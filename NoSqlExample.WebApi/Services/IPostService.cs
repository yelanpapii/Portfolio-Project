using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NoSqlExample.WebApi.Services
{
    public interface IPostService
    {
        public Task<IEnumerable<PostDTO>> GetAllPostAsync();
        public Task<PostDTO> GetSinglePostByIdAsync(int postId);
        public Task<IEnumerable<PostDTO>> GetAllPostByUserIdAsync(int userId);
    }
}
