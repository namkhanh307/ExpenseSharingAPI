using AutoMapper;
using Repositories.Entities;
using Repositories.ResponseModel.CalculateModel;
using Repositories.ResponseModel.PersonGroupModel;
using Repositories.ResponseModel.PersonModel;

namespace Services.Mapper
{
    public class PersonProfile : Profile
    {
        public PersonProfile()
        {
            CreateMap<Person, GetPersonModel>().ReverseMap();
            CreateMap<Person, PutPersonModel>().ReverseMap();
            CreateMap<Person, PostPersonModel>().ReverseMap();
            CreateMap<Person, GetPersonModel>()
            .ForMember(dest => dest.IsAdmin, opt => opt.MapFrom(src => src.PersonGroups.FirstOrDefault().IsAdmin));
            CreateMap<Person, PersonCalculatingModel>().ReverseMap();
        }

    }
}
