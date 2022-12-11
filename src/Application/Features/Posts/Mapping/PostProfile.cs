using AutoMapper;
using ObakiSite.Application.Domain.Entities;
using ObakiSite.Shared.DTO;

namespace ObakiSite.Application.Features.Posts.Mapping
{
    public class PostProfile : Profile
    {
        public PostProfile()
        {
            CreateMap<PostDTO, Post>().ReverseMap();
            CreateMap<TagDTO, Tag>();
        }
    }
}
