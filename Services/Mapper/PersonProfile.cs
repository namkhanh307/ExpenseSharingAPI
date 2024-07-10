using AutoMapper;
using Repositories.Entities;
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
        }

    }
}
