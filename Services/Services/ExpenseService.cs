using AutoMapper;
using Core.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Repositories.Entities;
using Repositories.IRepositories;
using Repositories.ResponseModel.ExpenseModel;
using Repositories.ResponseModel.PersonExpenseModel;
using Services.IServices;
using System.Text.RegularExpressions;
using Group = Repositories.Entities.Group;

namespace Services.Services
{
    public class ExpenseService(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor contextAccessor) : IExpenseService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;
        private readonly IHttpContextAccessor _contextAccessor = contextAccessor;
        private string currentUserId => Authentication.GetUserIdFromHttpContextAccessor(_contextAccessor);

        public async Task<List<GetExpenseModel?>> GetExpenses(string? reportId, string? type, DateTime? fromDate, DateTime? endDate, string? expenseName)
        {
            IQueryable<Expense> query = _unitOfWork.GetRepository<Expense>().Entities.Where(g => !g.DeletedTime.HasValue).AsQueryable();
            if (!string.IsNullOrWhiteSpace(reportId))
            {
                query = query.Where(e => e.ReportId == reportId);
            }
            if(!string.IsNullOrEmpty(type))
            {
                query = query.Where(e => e.Type == type);
            }
            if(fromDate != null)
            {
                query = query.Where(e => e.CreatedTime >= fromDate);
            }
            if(endDate != null)
            {
                query = query.Where(e => e.CreatedTime <= endDate);
            }
            if (!string.IsNullOrEmpty(expenseName))
            {
                query = query.Where(e => e.Name == expenseName);
            }        
            return _mapper.Map<List<GetExpenseModel?>>(await query.ToListAsync());
        }

        public async Task PostExpense(PostExpenseModel model)
        {
            string idUser = Authentication.GetUserIdFromHttpContextAccessor(_contextAccessor);
            string expenseId = Guid.NewGuid().ToString("N");

            string fileName = await FileUploadHelper.UploadFile(model.InvoiceImage, expenseId);
            Expense newExpense = new Expense()
            {
                Id = expenseId,
                Report = _unitOfWork.GetRepository<Report>().Entities.Where(r => r.Id == model.ReportId).FirstOrDefault(),
                Name = model.Name,
                Type = model.Type,
                Amount = model.Amount,
                ReportId = model.ReportId,
                CreatedTime = DateTime.Now,
                CreatedBy = idUser,
                InvoiceImage = fileName
            };
            ICollection<PersonGroup> personGroup = newExpense.Report.Group.PersonGroups;
            ICollection<Person> person = personGroup.Select(pg => pg.Person).ToList();
            foreach (var item in person)
            {
                var newPersonExpense = new PersonExpense()
                {
                    ExpenseId = expenseId,
                    PersonId = item.Id,
                    Amount = 0,
                    CreatedTime = DateTime.Now,                 
                };
                await _unitOfWork.GetRepository<PersonExpense>().InsertAsync(newPersonExpense);
            }      
            await _unitOfWork.GetRepository<Expense>().InsertAsync(newExpense);
            await _unitOfWork.SaveAsync();
        }

        public async Task PutExpense(PutExpenseModel model)
        {
            var existedExpense = await _unitOfWork.GetRepository<Expense>().GetByIdAsync(model.Id);

            if (existedExpense == null)
            {
                throw new Exception($"Expense with ID {model.Id} doesn't exist!");
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
            ICollection<PersonExpense> personExpenses = _unitOfWork.GetRepository<PersonExpense>().Entities.Where(pe => pe.ExpenseId == model.Id).ToList();
            foreach (var item in personExpenses)
            {
                await _unitOfWork.PersonExpenseRepository.DeletePersonExpense(item.PersonId, item.ExpenseId);
                await _unitOfWork.SaveAsync();
            }
            ICollection<PersonGroup> personGroup = existedExpense.Report.Group.PersonGroups;
            ICollection<Person> person = personGroup.Select(pg => pg.Person).ToList();
            foreach (var item in person)
            {
                var newPersonExpense = new PersonExpense()
                {
                    ExpenseId = model.Id,
                    PersonId = item.Id,
                    Amount = 0,
                    CreatedTime = DateTime.Now,
                };
                await _unitOfWork.GetRepository<PersonExpense>().InsertAsync(newPersonExpense);
            }
            existedExpense.LastUpdatedTime = DateTime.Now;
            existedExpense.LastUpdatedBy = currentUserId;
            await _unitOfWork.GetRepository<Expense>().UpdateAsync(existedExpense);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteExpense(string expenseId)
        {
            var existedExpense = await _unitOfWork.GetRepository<Expense>().GetByIdAsync(expenseId)
                                 ?? throw new Exception($"Expense with ID {expenseId} doesn't exist!");

            var groupId = await _unitOfWork.GetRepository<Report>()
                            .Entities.Where(r => r.Id == existedExpense.ReportId)
                            .Select(e => e.GroupId)
                            .FirstOrDefaultAsync();

            bool? isAdmin = false;
            if (new[] { "0", "1" }.Contains(existedExpense.Type))
            {
                isAdmin = await _unitOfWork.GetRepository<PersonGroup>()
                        .Entities.Where(p => p.PersonId.Equals(currentUserId) && p.GroupId.Equals(groupId))
                        .Select(p => p.IsAdmin).FirstAsync();
            }

            if (isAdmin.GetValueOrDefault().Equals(true) || existedExpense.CreatedBy == currentUserId)
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
                throw new Exception($"You don't have permission to delete this expense!");
            }
        }
    }
}
