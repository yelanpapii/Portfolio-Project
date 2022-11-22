using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using NoSqlExample.WebApi.Models;

namespace NoSqlExample.WebApi.NoSqlRepository
{
    public partial class PostRepository
    {
        private readonly IMongoCollection<Post> _mongoCollection;
        private readonly IConfiguration _configuration;

        public PostRepository(IConfiguration configuration
           )
        {
            _configuration = configuration;

            //Mongo Client
            var mongoClient = new MongoClient(_configuration.GetConnectionString("MongoDb"));
            var postDatabase = mongoClient.GetDatabase("PostDb");

            _mongoCollection = postDatabase.GetCollection<Post>("posts");
        }
    }
}