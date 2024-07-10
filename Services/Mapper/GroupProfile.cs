using AutoMapper;
using Repositories.Entities;
using Repositories.ResponseModel.GroupModel;

namespace Services.Mapper
{
    public class GroupProfile : Profile
    {
        public GroupProfile()
        {
            CreateMap<Group, GetGroupModel>().ReverseMap();
            CreateMap<Group, PutGroupModel>().ReverseMap();
            CreateMap<Group, PostGroupModel>().ReverseMap();
        }
    }
}
