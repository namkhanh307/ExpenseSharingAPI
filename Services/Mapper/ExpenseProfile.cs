using AutoMapper;
using Repositories.Entities;
using Repositories.ResponseModel.ExpenseModel;

namespace Services.Mapper
{
    public class ExpenseProfile : Profile
    {
        public ExpenseProfile()
        {
            CreateMap<Expense, GetExpenseModel>().ReverseMap();
            CreateMap<Expense, PutExpenseModel>().ReverseMap();
            CreateMap<Expense, PostExpenseModel>().ReverseMap();
        }
    }
}
