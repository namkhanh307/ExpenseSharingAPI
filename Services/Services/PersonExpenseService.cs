using AutoMapper;
using Core.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Repositories.Entities;
using Repositories.IRepositories;
using Repositories.ResponseModel.ExpenseModel;
using Repositories.ResponseModel.PersonExpenseModel;
using Repositories.ResponseModel.PersonGroupModel;
using Repositories.ResponseModel.PersonModel;
using Services.IServices;

namespace Services.Services
{
    public class PersonExpenseService : IPersonExpenseService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PersonExpenseService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<GetPersonExpenseModel> GetPersonExpenses(string reportId, string? expenseId)
        {
            var query = _unitOfWork.GetRepository<PersonExpense>().Entities
                .Where(pe => !pe.DeletedTime.HasValue)
                .Include(pe => pe.Expense)
                .ThenInclude(e => e.Report)
                .ThenInclude(r => r.Group)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(reportId))
            {
                query = query.Where(pe => pe.Expense.ReportId == reportId);
            }
            if (!string.IsNullOrWhiteSpace(expenseId))
            {
                query = query.Where(pe => pe.ExpenseId == expenseId);
            }

            var groupedResults = (await query.ToListAsync())
                                  .GroupBy(pe => pe.ExpenseId)
                                  .ToList();

            var firstGroup = groupedResults.FirstOrDefault();
            if (firstGroup == null)
            {
                throw new ErrorException(StatusCodes.Status404NotFound, ErrorCode.NotFound, "Không tìm thấy nhóm chi tiêu nào.");
            }

            var firstExpense = firstGroup.First().Expense;
            var group = firstExpense.Report.Group;

            var personGroups = group.PersonGroups.AsQueryable();

            var personList = personGroups
                .Select(pg => new GetPersonModel
                {
                    Id = pg.Person.Id,
                    Name = pg.Person.Name,
                    Phone = pg.Person.Phone,
                    Password = pg.Person.Password,
                    IsAdmin = pg.IsAdmin
                }).OrderBy(pg => pg.Name).ToList();

            var report = group.Reports.FirstOrDefault(r => r.Id == firstExpense.ReportId);

            var responseList = new GetPersonExpenseModel
            {
                Persons = personList,
                ReportName = report?.Name,
                ReportId = report.Id,
                PersonSubs = groupedResults.Select(group =>
                {
                    var firstExpense = group.First().Expense;
                    return new GetPersonExpenseSubSub
                    {
                        ExpenseId = group.Key,
                        ExpenseName = firstExpense.Name,
                        ExpenseAmount = firstExpense.Amount,
                        ExpenesePaidBy = group.Where(pe => pe.Amount > 0).Select(pe => pe.Person.Name).ToList(),
                        ExpenseCreatedTime = firstExpense.CreatedTime,
                        PersonExpenseSub = group.Select(pe => new GetPersonExpenseSubSubModel
                        {
                            Amount = pe.Amount,
                            IsShared = pe.IsShared,
                            Name = pe.Person.Name,
                            Id = pe.PersonId,
                            Phone = pe.Person.Phone,
                        }).OrderBy(pg => pg.Name).ToList(),
                    };
                }).ToList()
            };
            return responseList;
        }

        public async Task PostPersonExpense(PostPersonExpenseModel model)
        {
            foreach (var item in model.Persons)
            {
                var personExpense = new PersonExpense
                {
                    ExpenseId = model.ExpenseId,
                    PersonId = item.Id,
                    IsShared = item.IsShared,
                    Amount = item.Amount,
                    CreatedTime = DateTime.Now
                };
                await _unitOfWork.GetRepository<PersonExpense>().InsertAsync(personExpense);
            }
            await _unitOfWork.SaveAsync();
        }

        public async Task PostPersonExpenseForDeveloping(PostPersonExpenseForDevModel model)
        {
            var personExpense = new PersonExpense
            {
                ExpenseId = model.ExpenseId,
                PersonId = model.PersonId,
                IsShared = model.IsShared,
                Amount = model.Amount,
                CreatedTime = DateTime.Now
            };
            await _unitOfWork.GetRepository<PersonExpense>().InsertAsync(personExpense);
            await _unitOfWork.SaveAsync();
        }

        public async Task PutPersonExpense(string expenseId, PutPersonExpenseModel model)
        {
            List<PersonExpense> existedPersonExpenses = await _unitOfWork.GetRepository<PersonExpense>()
                                                            .Entities
                                                            .Where(e => e.ExpenseId == expenseId && !e.DeletedTime.HasValue)
                                                            .ToListAsync();

            if (existedPersonExpenses == null || existedPersonExpenses.Count == 0)
            {
                throw new ErrorException(StatusCodes.Status404NotFound, ErrorCode.NotFound, "Không tìm thấy chi tiêu của người nào.");
            }

            foreach (var item in existedPersonExpenses)
            {
                await _unitOfWork.PersonExpenseRepository.DeletePersonExpense(item.PersonId, item.ExpenseId);
            }

            await _unitOfWork.SaveAsync();

            foreach (var item in existedPersonExpenses)
            {
                _unitOfWork.GetRepository<PersonExpense>().Detach(item);
            }

            foreach (var item in model.Persons)
            {
                var personExpense = new PersonExpense()
                {
                    ExpenseId = expenseId,
                    PersonId = item.Id!,
                    IsShared = item.IsShared,
                    Amount = item.Amount,
                    CreatedTime = DateTime.Now
                };

                await _unitOfWork.GetRepository<PersonExpense>().InsertAsync(personExpense);
            }

            await _unitOfWork.SaveAsync();
        }

        public async Task DeletePersonExpense(string expenseId, string personId)
        {
            PersonExpense? existedPersonExpense = _unitOfWork.GetRepository<PersonExpense>().Entities
                .Where(pe => pe.ExpenseId == expenseId && pe.PersonId == personId)
                .FirstOrDefault();

            if (existedPersonExpense == null)
            {
                throw new ErrorException(StatusCodes.Status404NotFound, ErrorCode.NotFound, "Chi tiêu của người này không tồn tại.");
            }

            existedPersonExpense.DeletedTime = DateTime.Now;
            await _unitOfWork.GetRepository<PersonExpense>().UpdateAsync(existedPersonExpense);
            await _unitOfWork.SaveAsync();
        }
    }
}
