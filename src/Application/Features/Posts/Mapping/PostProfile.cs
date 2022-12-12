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

            CreateMap<PostSummary, PostSummaryDTO>()
                   .ForMember(dest => dest.Id,
                    opt => opt.MapFrom(src => src.Id))
                     .ForMember(dest => dest.Author,
                    opt => opt.MapFrom(src => src.Author))
                       .ForMember(dest => dest.Title,
                    opt => opt.MapFrom(src => src.Title))
                         .ForMember(dest => dest.CreationDate,
                    opt => opt.MapFrom(src => src.CreationDate));
        }
    }
}
