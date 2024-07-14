using AutoMapper;
using Core.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Authentication;
using Repositories.Entities;
using Repositories.IRepositories;
using Repositories.ResponseModel.ExpenseModel;
using Services.IServices;

namespace Services.Services
{
    public class ExpenseService : IExpenseService
    {
        private readonly IHttpContextAccessor _contextAccessor;

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ExpenseService(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor contextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _contextAccessor = contextAccessor;
        }

        public List<GetExpenseModel> GetExpenses(string? reportId, string? type)
        {
            var query = _unitOfWork.GetRepository<Expense>().Entities.Where(g => !g.DeletedTime.HasValue).AsQueryable();
            if (!string.IsNullOrWhiteSpace(reportId))
            {
                query = query.Where(e => e.ReportId == reportId);
            }
            if(!string.IsNullOrWhiteSpace(type))
            {
                query = query.Where(e => e.Type == type);
            }
            return _mapper.Map<List<GetExpenseModel>>(query.ToList());
        }
        public void PostExpense(PostExpenseModel model)
        {
            string idUser = Authentication.GetUserIdFromHttpContextAccessor(_contextAccessor);
            var expense = _mapper.Map<Expense>(model);
            expense.CreatedTime = DateTime.Now;
            expense.CreatedBy = idUser;
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
