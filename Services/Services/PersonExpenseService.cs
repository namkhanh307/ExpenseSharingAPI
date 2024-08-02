using AutoMapper;
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
        private readonly IMapper _mapper;
        public PersonExpenseService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<GetPersonExpenseModel> GetPersonExpenses(string? reportId, string? expenseId)
        {
            // Fetch the main query
            var query = _unitOfWork.GetRepository<PersonExpense>().Entities
                .Where(pe => !pe.DeletedTime.HasValue)
                .Include(pe => pe.Person)
                .Include(pe => pe.Expense)
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
            var groupedResults = query.ToList()
                                      .GroupBy(pe => pe.ExpenseId)
                                      .ToList();
            Group group = query.FirstOrDefault().Expense.Report.Group;
            IQueryable<PersonGroup> personGroups = group.PersonGroups.AsQueryable();
            List<GetPersonModel> personList = personGroups
                .Select(pg => new GetPersonModel
                {
                    Id = pg.Person.Id,
                    Name = pg.Person.Name,
                    Phone = pg.Person.Phone,
                    Password = pg.Person.Password,
                    IsAdmin = pg.IsAdmin
                }).OrderBy(pg => pg.Name).ToList();
            var report = group.Reports.FirstOrDefault();

            GetPersonExpenseModel responseList = new();
            responseList.Persons = personList;
            responseList.ReportName = report.Name;
            responseList.ReportId = reportId;
            // Prepare response
            var personExpenseSub = groupedResults.Select(group =>
            {
                var firstExpense = group.First().Expense;

                //var affordedBy = _unitOfWork.GetRepository<Person>().GetById(firstExpense.CreatedBy);
                return new GetPersonExpenseSubSub
                {
                    ExpenseId = group.Key,
                    ExpenseName = firstExpense.Name,
                    ExpenseAmount = firstExpense.Amount,
                    ExpenseCreatedTime = firstExpense.CreatedTime,
                    PersonExpenseSub = group.Select(pe => {
                        return new GetPersonExpenseSubSubModel()
                        {
                            Amount = pe.Amount,
                            IsShared = pe.IsShared,
                            Name = pe.Person.Name,
                            Id = pe.PersonId,
                            Phone = pe.Person.Phone,
                        };
                    }).OrderBy(pg => pg.Name).ToList(),
                };
            }).ToList();
            responseList.PersonSubs = personExpenseSub;
            return responseList;
        }

        public async Task PostPersonExpense(PostPersonExpenseModel model)
        {
            foreach (var item in model.Persons)
            {
                var personExpense = new PersonExpense()
                {
                    ExpenseId = model.ExpenseId,
                    PersonId = item.Id,
                    //ReportId = model.ReportId,
                    IsShared = item.IsShared,
                    Amount = item.Amount,
                };
                personExpense.CreatedTime = DateTime.Now;
                _unitOfWork.GetRepository<PersonExpense>().Insert(personExpense);
            }           
            _unitOfWork.Save();
        }
        public async Task PostPersonExpenseForDeveloping(PostPersonExpenseForDevModel model)
        {
            var personExpense = new PersonExpense()
            {
                ExpenseId = model.ExpenseId,
                PersonId = model.PersonId,
                //ReportId = model.ReportId,
                IsShared = model.IsShared,
                Amount = model.Amount,
            };
            personExpense.CreatedTime = DateTime.Now;
            _unitOfWork.GetRepository<PersonExpense>().Insert(personExpense);
            _unitOfWork.Save();       
        }

        public async Task PutPersonExpense(string expenseId, PutPersonExpenseModel model)
        {
            // Fetch existing PersonExpense entities
            var existedPersonExpenses = _unitOfWork.GetRepository<PersonExpense>()
                                                   .Entities
                                                   .Where(e => e.ExpenseId == expenseId && !e.DeletedTime.HasValue)
                                                   .ToList();

            if (existedPersonExpenses == null || !existedPersonExpenses.Any())
            {
                throw new Exception($"This expense with Id {expenseId} doesn't have any person sharing with!");
            }

            // Mark existing PersonExpense entities as deleted
            foreach (var item in existedPersonExpenses)
            {
                _unitOfWork.PersonExpenseRepository.DeletePersonExpense(item.PersonId, item.ExpenseId);
            }

            // Save changes and detach the updated entities
            _unitOfWork.Save();
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

                _unitOfWork.GetRepository<PersonExpense>().Insert(personExpense);
            }

            // Save all changes
            _unitOfWork.Save();
        }

        public async Task DeletePersonExpense(string expenseId, string personId)
        {
            var existedPersonExpense = _unitOfWork.GetRepository<PersonExpense>().Entities.Where(pe => pe.ExpenseId == expenseId && pe.PersonId == personId).FirstOrDefault();
            if (existedPersonExpense == null)
            {
                throw new Exception($"PersonExpense with expenseId {expenseId} or personId {personId} doesn't exist!");
            }
            existedPersonExpense.DeletedTime = DateTime.Now;
            _unitOfWork.GetRepository<PersonExpense>().Update(existedPersonExpense);
            _unitOfWork.Save();
        }
    }
}
