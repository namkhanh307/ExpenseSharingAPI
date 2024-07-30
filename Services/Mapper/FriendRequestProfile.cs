using AutoMapper;
using Repositories.Entities;
using Repositories.ResponseModel.FriendRequestModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Mapper
{
    public class FriendRequestProfile : Profile
    {
        public FriendRequestProfile()
        {
            CreateMap<FriendRequest, GetFriendRequestModel>().ReverseMap()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));
            CreateMap<FriendRequest, PostFriendRequestModel>().ReverseMap();
        }
    }
}
