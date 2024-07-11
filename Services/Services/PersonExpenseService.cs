using AutoMapper;
using Repositories.Entities;
using Repositories.IRepositories;
using Repositories.ResponseModel.ExpenseModel;
using Repositories.ResponseModel.PersonExpenseModel;
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

        public List<GetPersonExpenseModel> GetPersonExpenses()
        {
            return _mapper.Map<List<GetPersonExpenseModel>>(_unitOfWork.GetRepository<PersonExpense>().Entities.Where(g => !g.DeletedTime.HasValue).ToList());
        }

        public void PostPersonExpense(PostPersonExpenseModel model)
        {
            var personExpense = _mapper.Map<PersonExpense>(model);
            personExpense.CreatedTime = DateTime.Now;
            _unitOfWork.GetRepository<PersonExpense>().Insert(personExpense);
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
        public void DeletePersonExpense(string id)
        {
            var existedPersonExpense = _unitOfWork.GetRepository<PersonExpense>().GetById(id);
            if (existedPersonExpense == null)
            {
                throw new Exception($"Group with ID {id} doesn't exist!");
            }
            existedPersonExpense.DeletedTime = DateTime.Now;
            _unitOfWork.GetRepository<PersonExpense>().Update(existedPersonExpense);
            _unitOfWork.Save();
        }
    }
}
