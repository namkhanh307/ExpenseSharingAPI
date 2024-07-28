using AutoMapper;
using Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repositories.ResponseModel.FriendModel;

namespace Services.Mapper
{
    public class FriendProfile : Profile
    {
        public FriendProfile()
        {
            CreateMap<Friend, GetFriendModel>().ReverseMap();
            CreateMap<Friend, PostFriendModel>().ReverseMap();
        }
    }
}
