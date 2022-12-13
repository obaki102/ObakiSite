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

        #region Writes
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

                await response.WriteAsJsonAsync(result);
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

                await response.WriteAsJsonAsync(result);
                return response;

            }
            catch (Exception ex)
            {
                await response.WriteAsJsonAsync(ApplicationResponse.Fail(ex.Message));
                return response;
            }
        }

        [Function("DeletePost")]
        public async Task<HttpResponseData> DeletePost([HttpTrigger(AuthorizationLevel.Function, "delete", Route = "deletePost/{id?}")] HttpRequestData req, string id)
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

                await response.WriteAsJsonAsync(result);
                return response;

            }
            catch (Exception ex)
            {
                await response.WriteAsJsonAsync(ApplicationResponse.Fail(ex.Message));
                return response;
            }
        }
        #endregion

        #region Reads
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

                await response.WriteAsJsonAsync(result);
                return response;

            }
            catch (Exception ex)
            {
                await response.WriteAsJsonAsync(ApplicationResponse.Fail(ex.Message));
                return response;
            }
        }

        [Function("GetAllPostSummaries")]
        public async Task<HttpResponseData> GetAllPostSummaries([HttpTrigger(AuthorizationLevel.Function, "get", Route = "getPostSummaries")] HttpRequestData req)
        {
            _logger.LogInformation("PostFunction trigger function processed a request.");

            var response = req.CreateResponse(HttpStatusCode.OK);

            try
            {
                var result = await _postService.GetAllPostSummaries();

                await response.WriteAsJsonAsync(result);
                return response;

            }
            catch (Exception ex)
            {
                await response.WriteAsJsonAsync(ApplicationResponse.Fail(ex.Message));
                return response;
            }
        }
        #endregion
    }
}
