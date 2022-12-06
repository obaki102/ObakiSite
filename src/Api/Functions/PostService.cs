using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using ObakiSite.Application.Domain.Entities;
using ObakiSite.Application.Features.Posts.Services;

namespace ObakiSite.Api.Functions
{
    public class PostService
    {
        private readonly ILogger _logger;
        private readonly IPostService _postService;

        public PostService(ILoggerFactory loggerFactory, IPostService postService)
        {
            _logger = loggerFactory.CreateLogger<PostService>();
            _postService = postService;
        }

        [Function("Posts")]
        public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequestData req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            var response = req.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Content-Type", "text/plain; charset=utf-8");
            var post = new Post
            {  
                    Id = new Guid().ToString(),
                     Title = "TEST POST",
                     HtmlBody ="BODY"

            };

            await _postService.CreatePost(post);
            response.WriteString("Welcome to Azure Functions!");

            return response;
        }
    }
}
