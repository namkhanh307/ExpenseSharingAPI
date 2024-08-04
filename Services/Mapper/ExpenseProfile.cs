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
            CreateMap<Expense, PutExpenseModel>().ReverseMap()
                .ForMember(dest => dest.Name, opt => opt.Condition(src => src.Name != null))
                .ForMember(dest => dest.Amount, opt => opt.Condition(src => src.Amount != null))
                .ForMember(dest => dest.InvoiceImage, opt => opt.Condition(src => src.InvoiceImage != null));
            CreateMap<Expense, PostExpenseModel>().ReverseMap();
        }
    }
}
