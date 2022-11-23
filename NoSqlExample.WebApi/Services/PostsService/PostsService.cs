using AutoMapper;
using Microsoft.Extensions.Logging;
using MongoDB.Driver.Linq;
using NoSqlExample.WebApi.NoSqlRepository;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace NoSqlExample.WebApi.Services.PostsService
{
    public partial class PostsService : IPostService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IPostRepository _postRepository;
        private readonly ILogger<PostsService> _logger;
        private readonly IMapper _mapper;

        public PostsService(IHttpClientFactory clientFactory,
            IPostRepository repository,
            IMapper mapper,
            ILogger<PostsService> logger)
        {
            _httpClientFactory = clientFactory;
            _postRepository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<PostDTO>> GetAllPostAsync()
        {
            var url = $"https://jsonplaceholder.typicode.com/posts";
            var httpClient = _httpClientFactory.CreateClient();
            var response = await httpClient.GetAsync(url);

            if (response.StatusCode == HttpStatusCode.NotFound) return null;

            var posts = await response.Content.ReadFromJsonAsync<IEnumerable<PostDTO>>();
            return posts;
        }

        public async Task<PostDTO> GetSinglePostByIdAsync(int postId)
        {
            var url = $"https://jsonplaceholder.typicode.com/posts/{postId}";
            var httpClient = _httpClientFactory.CreateClient();
            var response = await httpClient.GetAsync(url);

            if (response.StatusCode == HttpStatusCode.NotFound) return null;

            var post = await response.Content.ReadFromJsonAsync<PostDTO>();

            //Metodo paralelo para añadir a db
            Parallel.Invoke(async () =>
            {
                var currentPost = await _postRepository.FindOneAsync(post.id);
                if (currentPost is null)
                {
                    await this.BeginPostAdditionAsync(post);
                }
            });

            return post;
        }

        public async Task<IEnumerable<PostDTO>> GetAllPostByUserIdAsync(int userId)
        {
            var url = $"https://jsonplaceholder.typicode.com/posts";
            var httpClient = _httpClientFactory.CreateClient();
            var response = await httpClient.GetAsync(url);

            if (response.StatusCode == HttpStatusCode.NotFound) return null;

            var posts = await response.Content.ReadFromJsonAsync<ICollection<PostDTO>>();
            posts = posts.Where(x => x.userId == userId).ToList();

            //Metodo en paralelo para añadir a db
            Parallel.Invoke(async () =>
            {
                //lista filtrada de post mediante userId
                //List<Post> filter = _mapper.Map<List<Post>>(posts).Where(x => x.userId == userId).ToList()
                foreach (var post in posts)
                {
                    var currentPost = await _postRepository.FindOneAsync(post.id);

                    if (currentPost is null)
                    {
                        await this.BeginPostAdditionAsync(post);
                    }
                }
            });

            return posts;
        }
    }
}