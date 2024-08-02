using AutoMapper;
using Repositories.Entities;
using Repositories.IRepositories;
using Repositories.ResponseModel.ExpenseModel;
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
        public async Task<List<GetPersonModel>> GetPersons(string? id)
        {
            var response = _unitOfWork.GetRepository<Person>().Entities.Where(g => !g.DeletedTime.HasValue).ToList();
            if(!string.IsNullOrWhiteSpace(id))
            {
                response = response.Where(p => p.Id == id).ToList();
            }
            return _mapper.Map<List<GetPersonModel>>(response);
        }

        public async Task PostPerson(PostPersonModel model)
        {
            var person = _mapper.Map<Person>(model);
            person.CreatedTime = DateTime.Now;
            _unitOfWork.GetRepository<Person>().Insert(person);
            _unitOfWork.Save();
        }

        public async Task PutPerson(string id, PutPersonModel model)
        {
            var existedPerson = _unitOfWork.GetRepository<Person>().GetById(id);
            if (existedPerson == null)
            {
                throw new Exception($"Person with ID {id} doesn't exist!");
            }
            _mapper.Map(model, existedPerson);
            existedPerson.LastUpdatedTime = DateTime.Now;
            _unitOfWork.GetRepository<Person>().Update(existedPerson);
            _unitOfWork.Save();
        }

        public async Task DeletePerson(string id)
        {
            var existedPerson = _unitOfWork.GetRepository<Person>().GetById(id);
            if (existedPerson == null)
            {
                throw new Exception($"Person with ID {id} doesn't exist!");
            }
            existedPerson.DeletedTime = DateTime.Now;
            _unitOfWork.GetRepository<Person>().Update(existedPerson);
            _unitOfWork.Save();
        }

      
    }
}
