using Microsoft.Extensions.Logging;
using NoSqlExample.WebApi.Models;
using System;
using System.Threading.Tasks;

namespace NoSqlExample.WebApi.Services.PostsService
{
    public partial class PostsService
    {
        /// <summary>
        /// Begin the addition of the post into the db
        /// </summary>
        /// <param name="post"></param>
        /// <returns></returns>
        public async Task BeginPostAddition(Post post)
        {
            var ToDbpost = await _postRepository.FindOneAsync(post.id);
            if (ToDbpost is null)
            {
                await _postRepository.AddAsync(post);
                _logger.LogInformation("{DateTime}, Post N°{ObjectId} Has been succesfully added to the database", DateTime.Now, post.id);
            }
        }
    }
}