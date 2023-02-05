using AutoMapper;
using ObakiSite.Application.Domain.Entities;
using ObakiSite.Application.Shared.DTO;
using ObakiSite.Application.Infra.Data.Firebase;
using ObakiSite.Application.Shared;

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

        public async Task<Result> CreatePost(PostDTO post)
        {
            if(post is null)
                throw new ArgumentNullException(nameof(post));

            var checkIfPostAlreadyExist = await _firebaseProvider.Get<PostFirebase>(post.Id.ToString(), default).ConfigureAwait(false);

            if (checkIfPostAlreadyExist is not null)
            {
                return Result.Fail(new Error("PostFirebaseServiceError.CreatePost","Post already exist."));
            }

            var postDomain = _mapper.Map<PostFirebase>(post);
            await _firebaseProvider.AddOrUpdate(postDomain, default).ConfigureAwait(false);
            return Result.Success();

        }

        public async Task<Result> DeletePost(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentNullException(nameof(id));

            var postToDelete = await _firebaseProvider.Get<PostFirebase>(id.ToString(), default).ConfigureAwait(false);

            if (postToDelete is null)
            {
                return Result.Fail(new Error("PostFirebaseServiceError.DeletePost", "Post does not exist."));
            }

            await _firebaseProvider.Delete<PostFirebase>(id.ToString(), default).ConfigureAwait(false);
            return Result.Success();
        }
        public async Task<Result> UpdatePost(PostDTO post)
        {
            if (post is null)
                throw new ArgumentNullException(nameof(post));

            var postToUpdate = await _firebaseProvider.Get<PostFirebase>(post.Id.ToString(),default).ConfigureAwait(false);

            if (postToUpdate == null)
            {
                return Result.Fail(new Error("PostFirebaseServiceError.UpdatePost", "Post does not exist."));
            }

            var postDomain = _mapper.Map<PostFirebase>(post);
            postDomain.Created = post.Created.ToUniversalTime();
            postDomain.Modified= DateTime.UtcNow;

            await _firebaseProvider.AddOrUpdate(postDomain, default).ConfigureAwait(false);
            return Result.Success();
        }

        //Reads

        public async Task<Result<IReadOnlyList<PostSummaryDTO>>> GetAllPostSummaries()
        {
            var postSummary = (await _firebaseProvider.GetAll<PostFirebase>(default).ConfigureAwait(false))
                            .Select(p => new PostSummary { Id = Guid.Parse(p.Id), Author = p.Author, CreationDate = p.Created, Title = p.Title  }).ToList();

            if (postSummary is not null)
            {
                var postSummaryDTO = _mapper.Map<IReadOnlyList<PostSummaryDTO>>(postSummary);
                return Result.Success(postSummaryDTO);
            }

            return Result.Fail<IReadOnlyList<PostSummaryDTO>> (new Error("PostFirebaseServiceError.GetAllPostSummaries", "Unable to retrieve post summaries."));
        }

        public async Task<Result<PostDTO>> GetPostById(Guid  id)
        {
            if (id == Guid.Empty)
                throw new ArgumentNullException(nameof(id));

            var post = await _firebaseProvider.Get<PostFirebase>(id.ToString(), default).ConfigureAwait(false);
            if (post is not null)
            {
                var postDTO = _mapper.Map<PostDTO>(post);
                return postDTO;
            }
            return Result.Fail<PostDTO>(new Error("PostFirebaseServiceError.GetPostById",$"Post with id {id} - unable to retrieve."));
        }
    }
}
