using AutoMapper;
using Repositories.Entities;
using Repositories.IRepositories;
using Repositories.ResponseModel.PersonModel;
using Services.IServices;

namespace Services.Services
{
    public class PersonService : IPersonService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public PersonService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public void DeletePerson(string id)
        {
            throw new NotImplementedException();
        }

        public List<Person> GetPersons()
        {
            throw new NotImplementedException();
        }

        public void PostPerson(PostPersonModel model)
        {
            throw new NotImplementedException();
        }

        public void PutPerson(string id, PutPersonModel model)
        {
            throw new NotImplementedException();
        }
    }
}
