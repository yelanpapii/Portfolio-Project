using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NoSqlExample.WebApi.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NoSqlExample.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PostsController : ControllerBase
    {
        private readonly IPostService _postService;
        private readonly ILogger<PostsController> _logger;

        public PostsController(ILogger<PostsController> logger,
            IPostService service)
        {
            _logger = logger;
            _postService = service;
        }

        [HttpGet]
        public async Task<IEnumerable<object>> GetAllPost()
        {
            _logger.LogInformation("{time} Getting all the posts", DateTime.UtcNow.ToString());

            return await _postService.GetAllPostAsync();
        }

        [HttpGet("{id:int}")]
        public async Task<object> GetPostById(int id)
        {
            _logger.LogInformation("{time} Getting all the posts", DateTime.UtcNow.ToString());

            return await _postService.GetSinglePostByIdAsync(id);
        }

        [HttpGet("user/{id:int}")]
        public async Task<IEnumerable<object>> GetPostByUserId(int id)
        {
            _logger.LogInformation("{time} Getting all the posts", DateTime.UtcNow.ToString());

            return await _postService.GetAllPostByUserIdAsync(id);
        }
    }
}