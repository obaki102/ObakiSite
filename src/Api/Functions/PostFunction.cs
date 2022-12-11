using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using ObakiSite.Application.Features.Posts.Services;
using ObakiSite.Shared.DTO.Response;
using System.Text.Json;
using ObakiSite.Shared.DTO;

namespace ObakiSite.Api.Functions
{
    public class PostFunction
    {
        private readonly ILogger _logger;
        private readonly IPostService _postService;

        public PostFunction(ILoggerFactory loggerFactory, IPostService postService)
        {
            _logger = loggerFactory.CreateLogger<PostFunction>();
            _postService = postService;
        }

        [Function("CreatePost")]
        public async Task<HttpResponseData> CreatePost([HttpTrigger(AuthorizationLevel.Function, "post", Route = "createPost")] HttpRequestData req)
        {
            _logger.LogInformation("PostFunction trigger function processed a request.");

            var response = req.CreateResponse(HttpStatusCode.OK);
            var request = req.Body;
            if (request.Length == 0)
            {
                await response.WriteAsJsonAsync(ApplicationResponse.Fail("No data posted."));
                return response;
            }
            try
            {
                var post = await JsonSerializer.DeserializeAsync<PostDTO>(request);
                var result = await _postService.CreatePost(post);
                if (result.IsSuccess)
                {
                    await response.WriteAsJsonAsync(result);
                    return response;
                }

                await response.WriteAsJsonAsync(ApplicationResponse.Fail($"Post with id {post.Id} - creation failed."));
                return response;
            }
            catch (Exception ex)
            {
                await response.WriteAsJsonAsync(ApplicationResponse.Fail(ex.Message));
                return response;
            }
        }

        [Function("UpdatePost")]
        public async Task<HttpResponseData> UpdatePost([HttpTrigger(AuthorizationLevel.Function, "post", Route = "updatePost")] HttpRequestData req)
        {
            _logger.LogInformation("PostFunction trigger function processed a request.");

            var response = req.CreateResponse(HttpStatusCode.OK);
            var request = req.Body;
            if (request.Length == 0)
            {
                await response.WriteAsJsonAsync(ApplicationResponse.Fail("No data posted."));
                return response;
            }
            try
            {

                var post = await JsonSerializer.DeserializeAsync<PostDTO>(request);
                var result = await _postService.UpdatePost(post);
                if (result.IsSuccess)
                {
                    await response.WriteAsJsonAsync(result);
                    return response;
                }

                await response.WriteAsJsonAsync(ApplicationResponse.Fail($"Post with id {post.Id} - update  operation failed."));
                return response;
            }
            catch (Exception ex)
            {
                await response.WriteAsJsonAsync(ApplicationResponse.Fail(ex.Message));
                return response;
            }
        }

        [Function("DeletePost")]
        public async Task<HttpResponseData> DeletePost([HttpTrigger(AuthorizationLevel.Function, "post", Route = "deletePost/{id?}")] HttpRequestData req , string id)
        {
            _logger.LogInformation("PostFunction trigger function processed a request.");

            var response = req.CreateResponse(HttpStatusCode.OK);
            if (string.IsNullOrEmpty(id))
            {
                await response.WriteAsJsonAsync(ApplicationResponse.Fail("Invalid id."));
                return response;
            }
            try
            {
                var result = await _postService.DeletePost(id);
                if (result.IsSuccess)
                {
                    await response.WriteAsJsonAsync(result);
                    return response;
                }

                await response.WriteAsJsonAsync(ApplicationResponse.Fail($"Post with id {id} -  delete operation failed."));
                return response;
            }
            catch (Exception ex)
            {
                await response.WriteAsJsonAsync(ApplicationResponse.Fail(ex.Message));
                return response;
            }
        }

        [Function("GetPostById")]
        public async Task<HttpResponseData> GetPostById([HttpTrigger(AuthorizationLevel.Function, "get", Route = "getPost/{id?}")] HttpRequestData req, string id)
        {
            _logger.LogInformation("PostFunction trigger function processed a request.");

            var response = req.CreateResponse(HttpStatusCode.OK);
            if (string.IsNullOrEmpty(id))
            {
                await response.WriteAsJsonAsync(ApplicationResponse.Fail("Invalid id."));
                return response;
            }
            try
            {
                var result = await _postService.GetPostById(id);
                if (result.IsSuccess)
                {
                    await response.WriteAsJsonAsync(result);
                    return response;
                }

                await response.WriteAsJsonAsync(ApplicationResponse.Fail($"Post with id {id} - unable to retrieve."));
                return response;
            }
            catch (Exception ex)
            {
                await response.WriteAsJsonAsync(ApplicationResponse.Fail(ex.Message));
                return response;
            }
        }
    }
}
