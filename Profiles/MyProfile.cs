using AutoMapper;
using PostTags.DTOs;
using PostTags.Models;

namespace PostTags.Profiles
{
    public class MyProfile:Profile
    {
        public MyProfile()
        {
            CreateMap<Post, PostDTO>().ReverseMap();
            CreateMap<Tag, TagDTO>().ReverseMap();
        }
    }
}
