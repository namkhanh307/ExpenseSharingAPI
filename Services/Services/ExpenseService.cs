using AutoMapper;
using Core.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Repositories.Entities;
using Repositories.IRepositories;
using Repositories.ResponseModel.ExpenseModel;
using Repositories.ResponseModel.PersonExpenseModel;
using Services.IServices;
using System.Text.RegularExpressions;

namespace Services.Services
{
    public class ExpenseService : IExpenseService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IPersonExpenseService _personExpenseService;
        private readonly IHttpContextAccessor _contextAccessor;
        public ExpenseService(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor contextAccessor, IPersonExpenseService personExpenseService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _contextAccessor = contextAccessor;
            _personExpenseService = personExpenseService;   
        }

        public async Task<List<GetExpenseModel>> GetExpenses(string? reportId, string? type, DateTime? fromDate, DateTime? endDate, string? expenseName)
        {
            var query = _unitOfWork.GetRepository<Expense>().Entities.Where(g => !g.DeletedTime.HasValue).AsQueryable();

            query = query
                    .Where(e => string.IsNullOrWhiteSpace(reportId) || e.ReportId == reportId)
                    .Where(e => string.IsNullOrWhiteSpace(type) || e.Type == type)
                    .Where(e => fromDate == null || e.CreatedTime >= fromDate)
                    .Where(e => endDate == null || e.CreatedTime <= endDate)
                    .Where(e => string.IsNullOrWhiteSpace(expenseName) || e.Name.Contains(expenseName));

            return _mapper.Map<List<GetExpenseModel>>(await query.ToListAsync());
        }
        public async Task PostExpense(PostExpenseModel model)
        {
            string idUser = Authentication.GetUserIdFromHttpContextAccessor(_contextAccessor);
            var expenseId = Guid.NewGuid().ToString("N");
            string? fileName = await FileUploadHelper.UploadFile(model.InvoiceImage, expenseId);
            var newExpense = new Expense()
            {
                Id = expenseId,
                Report = null,
                Name = model.Name,
                Type = model.Type,
                Amount = model.Amount,
                ReportId = model.ReportId,
                CreatedTime = DateTime.Now,
                CreatedBy = idUser,
                InvoiceImage = fileName
            };
            await _unitOfWork.GetRepository<Expense>().InsertAsync(newExpense);
            await _unitOfWork.SaveAsync();
        }

        public async Task PutExpense(string id, PutExpenseModel model)
        {
            string fileName = await FileUploadHelper.UploadFile(model.InvoiceImage, id);
            var existedExpense = _unitOfWork.GetRepository<Expense>().GetById(id);

            if (existedExpense == null)
            {
                throw new Exception($"Expense with ID {id} doesn't exist!");
            }

            if (!string.IsNullOrWhiteSpace(fileName))
            {
                if (existedExpense.InvoiceImage != null)
                {
                    await FileUploadHelper.DeleteFile(existedExpense.InvoiceImage);
                }
                existedExpense.InvoiceImage = fileName;  
            }

            if (!model.Amount.Equals(null))
            {
                existedExpense.Amount = model.Amount;
            }

            await _unitOfWork.GetRepository<Expense>().UpdateAsync(existedExpense);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteExpense(string expenseId)
        {
            var currentUserId = Authentication.GetUserIdFromHttpContextAccessor(_contextAccessor);
            Guid.TryParse(currentUserId, out var id);

            var existedExpense = _unitOfWork.GetRepository<Expense>().GetById(expenseId)
                                 ?? throw new Exception($"Expense with ID {expenseId} doesn't exist!");

            var groupId = _unitOfWork.GetRepository<Report>()
                            .Entities.Where(r => r.Id == existedExpense.ReportId)
                            .Select(e => e.GroupId)
                            .FirstOrDefault();

            bool? isAdmin = false;
            if (new[] { "0", "1" }.Contains(existedExpense.Type))
            {
                isAdmin = _unitOfWork.GetRepository<PersonGroup>()
                        .Entities.Where(p => p.PersonId.Equals(currentUserId) && p.GroupId.Equals(groupId))
                        .Select(p => p.IsAdmin).First();
            }

            if (isAdmin.GetValueOrDefault().Equals(true) || existedExpense.CreatedBy == currentUserId)
            {
                existedExpense.DeletedBy = currentUserId;
                existedExpense.DeletedTime = DateTime.Now;
                await FileUploadHelper.DeleteFile(existedExpense.InvoiceImage);
                _unitOfWork.GetRepository<Expense>().Update(existedExpense);
                _unitOfWork.Save();
            }
            else
            {
                throw new Exception($"You don't have permission to delete this expense!");
            }
        }


    }
}
