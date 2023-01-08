using AutoMapper;
using ObakiSite.Application.Domain.Entities;
using ObakiSite.Application.Shared.DTO.Response;
using ObakiSite.Application.Shared.DTO;
using ObakiSite.Application.Infra.Data.Firebase;

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
            if(post is null)
                throw new ArgumentNullException(nameof(post));

            var checkIfPostAlreadyExist = await _firebaseProvider.Get<PostFirebase>(post.Id.ToString(), default).ConfigureAwait(false);

            if (checkIfPostAlreadyExist is not null)
            {
                return ApplicationResponse.Fail("Post already exist.");
            }

            var postDomain = _mapper.Map<PostFirebase>(post);
            await _firebaseProvider.AddOrUpdate(postDomain, default).ConfigureAwait(false);
            return ApplicationResponse.Success();

        }

        public async Task<ApplicationResponse> DeletePost(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentNullException(nameof(id));

            var postToDelete = await _firebaseProvider.Get<PostFirebase>(id.ToString(), default).ConfigureAwait(false);

            if (postToDelete is null)
            {
                return ApplicationResponse.Fail("Post does not exist.");
            }

            await _firebaseProvider.Delete<PostFirebase>(id.ToString(), default).ConfigureAwait(false);
            return ApplicationResponse.Success();
        }
        public async Task<ApplicationResponse> UpdatePost(PostDTO post)
        {
            if (post is null)
                throw new ArgumentNullException(nameof(post));

            var postToUpdate = await _firebaseProvider.Get<PostFirebase>(post.Id.ToString(),default).ConfigureAwait(false);

            if (postToUpdate == null)
            {
                return ApplicationResponse.Fail("Post does not exist.");
            }

            var postDomain = _mapper.Map<PostFirebase>(post);
            postDomain.Created = post.Created.ToUniversalTime();
            postDomain.Modified= DateTime.UtcNow;

            await _firebaseProvider.AddOrUpdate(postDomain, default).ConfigureAwait(false);
            return ApplicationResponse.Success();
        }

        //Reads

        public async Task<ApplicationResponse<IReadOnlyList<PostSummaryDTO>>> GetAllPostSummaries()
        {
            var postSummary = (await _firebaseProvider.GetAll<PostFirebase>(default).ConfigureAwait(false))
                            .Select(p => new PostSummary { Id = Guid.Parse(p.Id), Author = p.Author, CreationDate = p.Created, Title = p.Title  }).ToList();

            if (postSummary is not null)
            {
                var postSummaryDTO = _mapper.Map<IReadOnlyList<PostSummaryDTO>>(postSummary);
                return ApplicationResponse<IReadOnlyList<PostSummaryDTO>>.Success(postSummaryDTO);
            }

            return ApplicationResponse<IReadOnlyList<PostSummaryDTO>>.Fail("Unable to retrieve post summaries.");
        }

        public async Task<ApplicationResponse<PostDTO>> GetPostById(Guid  id)
        {
            if (id == Guid.Empty)
                throw new ArgumentNullException(nameof(id));

            var post = await _firebaseProvider.Get<PostFirebase>(id.ToString(), default).ConfigureAwait(false);
            if (post is not null)
            {
                var postDTO = _mapper.Map<PostDTO>(post);
                return ApplicationResponse<PostDTO>.Success(postDTO);
            }
            return ApplicationResponse<PostDTO>.Fail($"Post with id {id} - unable to retrieve.");
        }
    }
}
