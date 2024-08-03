using AutoMapper;
using DocumentFormat.OpenXml.Office2016.Drawing.Command;
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
            CreateMap<Group, PutGroupModel>().ReverseMap()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Size, opt => opt.MapFrom(src => src.Size))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type));
            CreateMap<Group, PostGroupModel>().ReverseMap();
            CreateMap<Group, GetPersonGroupModel>()
                .ForMember(dest => dest.GroupId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Persons, opt => opt.MapFrom(src => src.PersonGroups.Select(pg => pg.Person)));
        }
    }
}