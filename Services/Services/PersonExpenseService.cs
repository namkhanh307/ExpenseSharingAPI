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
    public class PersonExpenseService : IPersonExpenseService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public PersonExpenseService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public List<GetPersonExpenseModel> GetPersonExpenses(string? reportId, string? expenseId)
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
                query = query.Where(pe => pe.ReportId == reportId);
            }
            if (!string.IsNullOrWhiteSpace(expenseId))
            {
                query = query.Where(pe => pe.ExpenseId == expenseId);
            }

            // Execute query and group results
            var groupedResults = query.ToList()
                                      .GroupBy(pe => pe.ExpenseId)
                                      .ToList();

            // Prepare response
            var responseList = groupedResults.Select(group =>
            {
                var firstExpense = group.First().Expense;
                var affordedBy = _unitOfWork.GetRepository<Person>().GetById(firstExpense.CreatedBy);
                return new GetPersonExpenseModel
                {
                    ExpenseId = group.Key,
                    ExpenseName = firstExpense.Name,
                    Persons = group.Select(pe => {
                        return new PersonExpenseModel(){
                            Amount = pe.Amount,
                            IsShared = pe.IsShared,
                            Name = pe.Person.Name,
                            Id = pe.PersonId,
                            Phone = pe.Person.Phone,
                        };
                    }).ToList(),
                    ReportId = firstExpense.ReportId,
                    ReportName = firstExpense.Report == null ? "Fix null" : firstExpense.Report.Name
                };
            }).ToList();

            return responseList;
        }


        public void PostPersonExpense(PostPersonExpenseModel model)
        {
            foreach (var item in model.Persons)
            {
                var personExpense = new PersonExpense()
                {
                    ExpenseId = model.ExpenseId,
                    PersonId = item.Id,
                    ReportId = model.ReportId,
                    IsShared = item.IsShared,
                    Amount = item.Amount,
                };
                personExpense.CreatedTime = DateTime.Now;
                _unitOfWork.GetRepository<PersonExpense>().Insert(personExpense);
            }           
            _unitOfWork.Save();
        }

        public void PutPersonExpense(string id, PutPersonExpenseModel model)
        {
            var existedPersonExpense = _unitOfWork.GetRepository<PersonExpense>().GetById(id);
            if (existedPersonExpense == null)
            {
                throw new Exception($"Group with ID {id} doesn't exist!");
            }
            _mapper.Map(model, existedPersonExpense);
            existedPersonExpense.LastUpdatedTime = DateTime.Now;
            _unitOfWork.GetRepository<PersonExpense>().Update(existedPersonExpense);
            _unitOfWork.Save();
        }
        public void DeletePersonExpense(string expenseId, string personId)
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
