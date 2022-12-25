using AutoMapper;
using MediatR;
using ObakiSite.Application.Extensions;
using ObakiSite.Application.Features.Posts.Constants;
using ObakiSite.Application.Shared.Constants;
using ObakiSite.Application.Shared.DTO;
using ObakiSite.Application.Shared.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ObakiSite.Application.Features.Posts.Commands
{
    public record UpdatePost(PostDTO Post) : IRequest<ApplicationResponse>;

    public class UpdatePostHandler : IRequestHandler<UpdatePost, ApplicationResponse>
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public UpdatePostHandler(IHttpClientFactory httpClientFactory, IMapper mapper)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<ApplicationResponse> Handle(UpdatePost request, CancellationToken cancellationToken)
        {
            var httpClient = _httpClientFactory.CreateClient(HttpNameClientConstants.Default);
            var serializedPost = JsonSerializer.Serialize(request.Post).ToJsonStringContent();
            var response = await httpClient.PutAsync(PostConstants.UpdatePost.EndPoint, serializedPost).ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.ConvertStreamToTAsync<ApplicationResponse>();

                if (result is not null)
                {
                    return result;
                }

                return ApplicationResponse.Fail("Unable to update post.");
            }

            return ApplicationResponse.Fail(response.StatusCode.ToString());
        }
    }
}

        
   
