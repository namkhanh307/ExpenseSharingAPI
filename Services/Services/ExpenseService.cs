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

        public List<GetExpenseModel> GetExpenses()
        {
            return _mapper.Map<List<GetExpenseModel>>(_unitOfWork.GetRepository<Expense>().Entities.Where(g => !g.DeletedTime.HasValue).ToList());
        }
        public void PostExpense(PostExpenseModel model)
        {
            var expense = _mapper.Map<Expense>(model);
            expense.CreatedTime = DateTime.Now;
            _unitOfWork.GetRepository<Expense>().Insert(expense);
            _unitOfWork.Save();
        }

        public void PutExpense(string id, PutExpenseModel model)
        {
            var existedExpense = _unitOfWork.GetRepository<Expense>().GetById(id);
            if (existedExpense == null)
            {
                throw new Exception($"Group with ID {id} doesn't exist!");
            }
            _mapper.Map(model, existedExpense);
            existedExpense.LastUpdatedTime = DateTime.Now;
            _unitOfWork.GetRepository<Expense>().Update(existedExpense);
            _unitOfWork.Save();
        }

        public void DeleteExpense(string id)
        {
            var existedExpense = _unitOfWork.GetRepository<Expense>().GetById(id);
            if (existedExpense == null)
            {
                throw new Exception($"Group with ID {id} doesn't exist!");
            }
            existedExpense.DeletedTime = DateTime.Now;
            _unitOfWork.GetRepository<Expense>().Update(existedExpense);
            _unitOfWork.Save();
        }
    }
}
