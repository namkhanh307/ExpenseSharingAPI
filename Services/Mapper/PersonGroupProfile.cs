using AutoMapper;
using Repositories.Entities;
using Repositories.ResponseModel.PersonGroupModel;

namespace Services.Mapper
{
    public class PersonGroupProfile : Profile
    {
        public PersonGroupProfile()
        {
            CreateMap<PersonGroup, GetPersonGroupModel>().ReverseMap();
            CreateMap<PersonGroup, PutPersonGroupModel>().ReverseMap();
            CreateMap<PersonGroup, PostPersonGroupModel>().ReverseMap();
        }

    }
}
