using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using ObakiSite.Application.Features.Posts.Services;
using ObakiSite.Application.Shared;
using System.Text.Json;
using ObakiSite.Application.Shared.DTO;
using Google.Api;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.OpenApi.Models;
using System.Collections.Generic;

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
        [OpenApiOperation(operationId: "CreatePost", tags: new[] { "Post" }, Summary = "Create new post.", Visibility = OpenApiVisibilityType.Important)]
        [OpenApiRequestBody(contentType: "application/json; charset=utf-8", bodyType: typeof(PostDTO), Description = "PostDTO", Required = true)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(bool), Summary = "Successful operation")]
        public async Task<HttpResponseData> CreatePost([HttpTrigger(AuthorizationLevel.Function, "post", Route = "post/create")] HttpRequestData req)
        {
            _logger.LogInformation("PostFunction trigger function processed a request.");

            var response = req.CreateResponse(HttpStatusCode.OK);
            var request = req.Body;

            if (request.Length == 0)
            {
                await response.WriteAsJsonAsync(Result.Fail("No data posted."));
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
                await response.WriteAsJsonAsync(Result.Fail(ex.Message));
                return response;
            }
        }

        [Function("UpdatePost")]
        [OpenApiOperation(operationId: "UpdatePost", tags: new[] { "Post" }, Summary = "Update a specified post.", Visibility = OpenApiVisibilityType.Important)]
        [OpenApiRequestBody(contentType: "application/json; charset=utf-8", bodyType: typeof(PostDTO), Description = "PostDTO", Required = true)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(bool), Summary = "Successful operation")]
        public async Task<HttpResponseData> UpdatePost([HttpTrigger(AuthorizationLevel.Function, "put", Route = "post/update")] HttpRequestData req)
        {
            _logger.LogInformation("PostFunction trigger function processed a request.");

            var response = req.CreateResponse(HttpStatusCode.OK);
            var request = req.Body;

            if (request.Length == 0)
            {
                await response.WriteAsJsonAsync(Result.Fail("No data posted."));
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
                await response.WriteAsJsonAsync(Result.Fail(ex.Message));
                return response;
            }
        }

        [Function("DeletePost")]
        [OpenApiOperation(operationId: "DeletePost", tags: new[] { "Post" }, Summary = "Delete a specified a post.", Visibility = OpenApiVisibilityType.Important)]
        [OpenApiRequestBody(contentType: "application/json; charset=utf-8", bodyType: typeof(PostDTO), Description = "PostDTO", Required = true)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(bool), Summary = "Successful operation")]
        public async Task<HttpResponseData> DeletePost([HttpTrigger(AuthorizationLevel.Function, "delete", Route = "post/delete/{id?}")] HttpRequestData req, string id)
        {
            _logger.LogInformation("PostFunction trigger function processed a request.");

            var response = req.CreateResponse(HttpStatusCode.OK);

            if (string.IsNullOrEmpty(id))
            {
                await response.WriteAsJsonAsync(Result.Fail("Invalid id."));
                return response;
            }

            try
            {
                var result = await _postService.DeletePost(Guid.Parse(id));

                await response.WriteAsJsonAsync(result);
                return response;

            }
            catch (Exception ex)
            {
                await response.WriteAsJsonAsync(Result.Fail(ex.Message));
                return response;
            }
        }
        #endregion

        #region Reads
        [Function("GetPostById")]
        [OpenApiOperation(operationId: "GetPostById", tags: new[] { "Post" }, Summary = "Retrieved a post by id.", Visibility = OpenApiVisibilityType.Important)]
        [OpenApiParameter(name: "id", In = ParameterLocation.Path, Required = true, Type = typeof(string), Description = "Valid  post id.")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(PostDTO), Summary = "Successful operation")]
        public async Task<HttpResponseData> GetPostById([HttpTrigger(AuthorizationLevel.Function, "get", Route = "post/get/{id?}")] HttpRequestData req, string id)
        {
            _logger.LogInformation("PostFunction trigger function processed a request.");

            var response = req.CreateResponse(HttpStatusCode.OK);

            if (string.IsNullOrEmpty(id))
            {
                await response.WriteAsJsonAsync(Result.Fail("Invalid id."));
                return response;
            }
            try
            {
                var result = await _postService.GetPostById(Guid.Parse(id));

                await response.WriteAsJsonAsync(result);
                return response;

            }
            catch (Exception ex)
            {
                await response.WriteAsJsonAsync(Result.Fail(ex.Message));
                return response;
            }
        }

        [Function("GetAllPostSummaries")]
        [OpenApiOperation(operationId: "GetAllPostSummaries", tags: new[] { "Post" }, Summary = "Retrieved all post summaries", Visibility = OpenApiVisibilityType.Important)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(IReadOnlyList<PostSummaryDTO>), Summary = "Successful operation")]
        public async Task<HttpResponseData> GetAllPostSummaries([HttpTrigger(AuthorizationLevel.Function, "get", Route = "post/get-summaries")] HttpRequestData req)
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
                await response.WriteAsJsonAsync(Result.Fail(ex.Message));
                return response;
            }
        }
        #endregion
    }
}
