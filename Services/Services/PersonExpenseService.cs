using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Repositories.Entities;
using Repositories.IRepositories;
using Repositories.ResponseModel.ExpenseModel;
using Repositories.ResponseModel.PersonExpenseModel;
using Repositories.ResponseModel.PersonModel;
using Services.IServices;

namespace Services.Services
{
    public class PersonExpenseService(IUnitOfWork unitOfWork) : IPersonExpenseService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<List<GetPersonExpenseModel>> GetPersonExpenses(string? reportId, string? expenseId)
        {
            // Fetch and filter the main query
            IQueryable<PersonExpense> query = _unitOfWork.GetRepository<PersonExpense>().Entities
                .Where(pe => !pe.DeletedTime.HasValue)
                .Include(pe => pe.Person)
                .Include(pe => pe.Expense)
                .ThenInclude(e => e.Report)
                .AsQueryable();

            // Apply filters
            if (!string.IsNullOrWhiteSpace(reportId))
            {
                query = query.Where(pe => pe.Expense.ReportId == reportId);
            }
            if (!string.IsNullOrWhiteSpace(expenseId))
            {
                query = query.Where(pe => pe.ExpenseId == expenseId);
            }

            // Execute query and fetch data
            var personExpenses = await query.ToListAsync();

            // Group results in-memory
            var groupedResults = personExpenses
                .GroupBy(pe => pe.ExpenseId);

            // Prepare response
            var responseList = groupedResults.Select(group =>
            {
                var firstExpense = group.First().Expense;
                return new GetPersonExpenseModel
                {
                    ExpenseId = group.Key,
                    ExpenseName = firstExpense.Name,
                    Persons = group.Select(pe => new GetPersonExpenseSub
                    {
                        Amount = pe.Amount,
                        IsShared = pe.IsShared,
                        Name = pe.Person.Name,
                        Id = pe.PersonId,
                        Phone = pe.Person.Phone,
                    }).ToList(),
                    ReportId = firstExpense.ReportId,
                    ReportName = firstExpense.Report?.Name ?? "Fix null"
                };
            }).ToList();
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
                    //ReportId = model.ReportId,
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
                //ReportId = model.ReportId,
                IsShared = model.IsShared,
                Amount = model.Amount,
                CreatedTime = DateTime.Now
            };
            await _unitOfWork.GetRepository<PersonExpense>().InsertAsync(personExpense);
            await _unitOfWork.SaveAsync();       
        }

        public async Task PutPersonExpense(string expenseId, PutPersonExpenseModel model)
        {
            // Fetch existing PersonExpense entities
            List<PersonExpense> existedPersonExpenses = await _unitOfWork.GetRepository<PersonExpense>()
                                                            .Entities
                                                            .Where(e => e.ExpenseId == expenseId && !e.DeletedTime.HasValue)
                                                            .ToListAsync();

            if (existedPersonExpenses == null || existedPersonExpenses.Count == 0)
            {
                throw new Exception($"This expense with Id {expenseId} doesn't have any person sharing with!");
            }

            // Mark existing PersonExpense entities as deleted
            foreach (var item in existedPersonExpenses)
            {
                await _unitOfWork.PersonExpenseRepository.DeletePersonExpense(item.PersonId, item.ExpenseId);
            }

            // Save changes and detach the updated entities
            await _unitOfWork.SaveAsync();
            foreach (var item in existedPersonExpenses)
            {
                _unitOfWork.GetRepository<PersonExpense>().Detach(item);
            }

            // Insert new PersonExpense entities
            foreach (var item in model.Persons)
            {
                var personExpense = new PersonExpense()
                {
                    ExpenseId = expenseId,
                    PersonId = item.Id!,
                    //ReportId = model.ReportId,
                    IsShared = item.IsShared,
                    Amount = item.Amount,
                    CreatedTime = DateTime.Now
                };

                await _unitOfWork.GetRepository<PersonExpense>().InsertAsync(personExpense);
            }

            // Save all changes
            await _unitOfWork.SaveAsync();
        }

        public async Task DeletePersonExpense(string expenseId, string personId)
        {
            PersonExpense? existedPersonExpense = _unitOfWork.GetRepository<PersonExpense>().Entities.Where(pe => pe.ExpenseId == expenseId && pe.PersonId == personId).FirstOrDefault();
            if (existedPersonExpense == null)
            {
                throw new Exception($"PersonExpense with expenseId {expenseId} or personId {personId} doesn't exist!");
            }
            existedPersonExpense.DeletedTime = DateTime.Now;
            await _unitOfWork.GetRepository<PersonExpense>().UpdateAsync(existedPersonExpense);
            await _unitOfWork.SaveAsync();
        }
    }
}
