using AutoMapper;
using Repositories.Entities;
using Repositories.IRepositories;
using Repositories.ResponseModel.ExpenseModel;
using Services.IServices;

namespace Services.Services
{
    public class ExpenseService : IExpenseService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ExpenseService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public void DeleteExpense(string id)
        {
            throw new NotImplementedException();
        }

        public List<Expense> GetExpenses()
        {
            throw new NotImplementedException();
        }

        public void PostExpense(PostExpenseModel model)
        {
            throw new NotImplementedException();
        }

        public void PutExpense(string id, PutExpenseModel model)
        {
            throw new NotImplementedException();
        }
    }
}
