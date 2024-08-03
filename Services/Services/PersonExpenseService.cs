﻿using AutoMapper;
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
    public class PersonExpenseService(IUnitOfWork unitOfWork) : IPersonExpenseService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<GetPersonExpenseModel> GetPersonExpenses(string reportId, string? expenseId)
        {
            // Fetch the main query with related entities
            var query = _unitOfWork.GetRepository<PersonExpense>().Entities
                .Where(pe => !pe.DeletedTime.HasValue)
                .Include(pe => pe.Expense)
                .ThenInclude(e => e.Report)
                .ThenInclude(r => r.Group)
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

            // Execute query and group results
            var groupedResults = (await query.ToListAsync())
                                  .GroupBy(pe => pe.ExpenseId)
                                  .ToList();

            var firstGroup = groupedResults.FirstOrDefault();
            if (firstGroup == null)
            {
                return null; // Handle the case where no results are found
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
                        //.Where(pe => pe.IsShared == true || pe.Amount > 0)
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
