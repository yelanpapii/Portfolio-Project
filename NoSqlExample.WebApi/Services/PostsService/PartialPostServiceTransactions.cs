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
        public async Task BeginPostAdditionAsync(PostDTO post)
        {
            var toDbPost = _mapper.Map<Post>(post);
            await _postRepository.AddAsync(toDbPost);
            _logger.LogInformation("{DateTime}, Post N°{ObjectId} Has been succesfully added to the database", DateTime.Now, post.id);
        }

        public async Task BeginPostDeleteAsync(int postId)
        {
            await _postRepository.DeleteAsync(postId);
            _logger.LogInformation("{DateTime}, Post N°{ObjectId} Has been succesfully deleted from the database", DateTime.Now, postId);
        }

        public async Task BeginPostUpdateAsync(int postId, PostDTO post) 
        {
            var toDbPost = _mapper.Map<Post>(post);
            await _postRepository.UpdateAsync(postId, toDbPost);
            _logger.LogInformation("{DateTime}, Post N°{ObjectId} Has been succesfully update from the database", DateTime.Now, postId);
        }
    }
}