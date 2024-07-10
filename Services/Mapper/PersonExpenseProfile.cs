using AutoMapper;
using Repositories.Entities;
using Repositories.ResponseModel.PersonExpenseModel;

namespace Services.Mapper
{
    public class PersonExpenseProfile : Profile
    {
        public PersonExpenseProfile()
        {
            CreateMap<PersonExpense, GetPersonExpenseModel>().ReverseMap();
            CreateMap<PersonExpense, PutPersonExpenseModel>().ReverseMap();
            CreateMap<PersonExpense, PostPersonExpenseModel>().ReverseMap();
        }

    }
}
