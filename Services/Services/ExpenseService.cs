using AutoMapper;
using Core.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Repositories.Entities;
using Repositories.IRepositories;
using Repositories.ResponseModel.ExpenseModel;
using Services.IServices;
using System.Text.RegularExpressions;

namespace Services.Services
{
    public class ExpenseService(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor contextAccessor) : IExpenseService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;
        private readonly IHttpContextAccessor _contextAccessor = contextAccessor;
        private string currentUserId => Authentication.GetUserIdFromHttpContextAccessor(_contextAccessor);

        public async Task<List<GetExpenseModel>> GetExpenses(string? reportId, string? type, DateTime? fromDate, DateTime? endDate, string? expenseName)
        {
            var query = _unitOfWork.GetRepository<Expense>().Entities.Where(g => !g.DeletedTime.HasValue);

            // Apply filters
            query = query.Where(e => string.IsNullOrWhiteSpace(reportId) || e.ReportId == reportId)
                         .Where(e => string.IsNullOrWhiteSpace(type) || e.Type == type)
                         .Where(e => fromDate == null || e.CreatedTime >= fromDate)
                         .Where(e => endDate == null || e.CreatedTime <= endDate)
                         .Where(e => string.IsNullOrWhiteSpace(expenseName) || e.Name!.Contains(expenseName));

            var expenses = await query.ToListAsync();

            return _mapper.Map<List<GetExpenseModel>>(expenses);
        }

        public async Task PostExpense(PostExpenseModel model)
        {
            var expenseId = Guid.NewGuid().ToString("N");
            string fileName = await FileUploadHelper.UploadFile(model.InvoiceImage, expenseId);
            var newExpense = new Expense()
            {
                Id = expenseId,
                Name = model.Name,
                Type = model.Type,
                Amount = model.Amount,
                ReportId = model.ReportId,
                CreatedTime = DateTime.Now,
                CreatedBy = currentUserId,
                InvoiceImage = fileName
            };

            await _unitOfWork.GetRepository<Expense>().InsertAsync(newExpense);
            await _unitOfWork.SaveAsync();
        }

        public async Task PutExpense(PutExpenseModel model)
        {
            var existedExpense = await _unitOfWork.GetRepository<Expense>().GetByIdAsync(model.Id);

            if (existedExpense == null)
            {
                throw new ErrorException(StatusCodes.Status404NotFound, ErrorCode.NotFound, "Chi tiêu không tồn tại!");
            }

            _mapper.Map(model, existedExpense);

            if (model.InvoiceImage != null)
            {
                string fileName = await FileUploadHelper.UploadFile(model.InvoiceImage, model.Id);
                if (existedExpense.InvoiceImage != null)
                {
                    FileUploadHelper.DeleteFile(existedExpense.InvoiceImage);
                }
                existedExpense.InvoiceImage = fileName;
            }

            existedExpense.LastUpdatedTime = DateTime.Now;
            existedExpense.LastUpdatedBy = currentUserId;
            await _unitOfWork.GetRepository<Expense>().UpdateAsync(existedExpense);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteExpense(string expenseId)
        {
            var existedExpense = await _unitOfWork.GetRepository<Expense>().GetByIdAsync(expenseId)
                ?? throw new ErrorException(StatusCodes.Status404NotFound, ErrorCode.NotFound, "Chi tiêu không tồn tại!");

            var groupId = await _unitOfWork.GetRepository<Report>()
                            .Entities.Where(r => r.Id == existedExpense.ReportId)
                            .Select(e => e.GroupId)
                            .FirstOrDefaultAsync();

            bool? isAdmin = false;
            if (new[] { "0", "1" }.Contains(existedExpense.Type))
            {
                isAdmin = await _unitOfWork.GetRepository<PersonGroup>()
                        .Entities.Where(p => p.PersonId.Equals(currentUserId) && p.GroupId.Equals(groupId))
                        .Select(p => p.IsAdmin).FirstOrDefaultAsync();
            }

            if (isAdmin.GetValueOrDefault() || existedExpense.CreatedBy == currentUserId)
            {
                existedExpense.DeletedBy = currentUserId;
                existedExpense.DeletedTime = DateTime.Now;
                if (existedExpense.InvoiceImage != null)
                {
                    FileUploadHelper.DeleteFile(existedExpense.InvoiceImage);
                }
                await _unitOfWork.GetRepository<Expense>().UpdateAsync(existedExpense);
                await _unitOfWork.SaveAsync();
            }
            else
            {
                throw new ErrorException(StatusCodes.Status403Forbidden, ErrorCode.UnAuthorized, "Bạn không có quyền xóa chi phí này!");
            }
        }
    }
}

