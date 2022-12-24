using AutoMapper;
using Google.Cloud.Firestore.V1;
using ObakiSite.Application.Domain.Entities;
using ObakiSite.Application.Infra.Data;
using ObakiSite.Shared.DTO.Response;
using ObakiSite.Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObakiSite.Application.Features.Posts.Services
{
    public class PostFirebaseService : IPostService
    {
        private readonly FirestoreProvider _firebaseProvider;
        private readonly IMapper _mapper;

        public PostFirebaseService(FirestoreProvider firebaseProvider, IMapper mapper)
        {
            _firebaseProvider = firebaseProvider;
            _mapper = mapper;
        }

        public async Task<ApplicationResponse> CreatePost(PostDTO post)
        {
            var checkIfPostAlreadyExist = await _firebaseProvider.Get<Post>(post.Id, default).ConfigureAwait(false);

            if (checkIfPostAlreadyExist is not null)
            {
                return ApplicationResponse.Fail("Post already exist.");
            }

            var postDomain = _mapper.Map<Post>(post);
            await _firebaseProvider.AddOrUpdate(postDomain, default).ConfigureAwait(false);
            return ApplicationResponse.Success();

        }

        public async Task<ApplicationResponse> DeletePost(string id)
        {
            var postToDelete = await _firebaseProvider.Get<Post>(id, default).ConfigureAwait(false);

            if (postToDelete is null)
            {
                return ApplicationResponse.Fail("Post does not exist.");
            }

            await _firebaseProvider.Delete<Post>(id, default).ConfigureAwait(false);
            return ApplicationResponse.Success();
        }
        public async Task<ApplicationResponse> UpdatePost(PostDTO post)
        {
            var postToUpdate = await _firebaseProvider.Get<Post>(post.Id,default).ConfigureAwait(false);

            if (postToUpdate == null)
            {
                return ApplicationResponse.Fail("Post does not exist.");
            }

            var postDomain = _mapper.Map<Post>(post);
            await _firebaseProvider.AddOrUpdate(postDomain, default).ConfigureAwait(false);
            return ApplicationResponse.Success();
        }

        //Reads

        public async Task<ApplicationResponse<IReadOnlyList<PostSummaryDTO>>> GetAllPostSummaries()
        {
            var postSummary = (await _firebaseProvider.GetAll<Post>(default).ConfigureAwait(false))
                            .Select(p => new PostSummary(p)).ToList();

            if (postSummary is not null)
            {
                var postSummaryDTO = _mapper.Map<IReadOnlyList<PostSummaryDTO>>(postSummary);
                return ApplicationResponse<IReadOnlyList<PostSummaryDTO>>.Success(postSummaryDTO);
            }

            return ApplicationResponse<IReadOnlyList<PostSummaryDTO>>.Fail("Unable to retrieve post summaries.");
        }

        public async Task<ApplicationResponse<PostDTO>> GetPostById(string id)
        {
            var post = await _firebaseProvider.Get<Post>(id, default).ConfigureAwait(false);
            if (post is not null)
            {
                var postDTO = _mapper.Map<PostDTO>(post);
                return ApplicationResponse<PostDTO>.Success(postDTO);
            }
            return ApplicationResponse<PostDTO>.Fail($"Post with id {id} - unable to retrieve.");
        }
    }
}
