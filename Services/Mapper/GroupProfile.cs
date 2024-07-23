using AutoMapper;
using Repositories.Entities;
using Repositories.ResponseModel.GroupModel;
using Repositories.ResponseModel.PersonGroupModel;

namespace Services.Mapper
{
    public class GroupProfile : Profile
    {
        public GroupProfile()
        {
            CreateMap<Group, GetGroupModel>().ReverseMap();
            CreateMap<Group, PutGroupModel>().ReverseMap();
            CreateMap<Group, PostGroupModel>().ReverseMap();
            CreateMap<Group, GetPersonGroupModel>()
                .ForMember(dest => dest.GroupId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Persons, opt => opt.MapFrom(src => src.PersonGroups.Select(pg => pg.Person)));
        }
    }
}