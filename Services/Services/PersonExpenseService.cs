using AutoMapper;
using Repositories.Entities;
using Repositories.IRepositories;
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

        public void DeletePersonExpense(string id)
        {
            throw new NotImplementedException();
        }

        public List<PersonExpense> GetPersonExpenses()
        {
            throw new NotImplementedException();
        }

        public void PostPersonExpense(PostPersonExpenseModel model)
        {
            throw new NotImplementedException();
        }

        public void PutPersonExpense(string id, PutPersonExpenseModel model)
        {
            throw new NotImplementedException();
        }
    }
}
