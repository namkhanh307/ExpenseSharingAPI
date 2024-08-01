using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Repositories.Entities;
using Repositories.IRepositories;
using Repositories.ResponseModel.ExpenseModel;
using Repositories.ResponseModel.PersonModel;
using Services.IServices;

namespace Services.Services
{
    public class PersonService(IUnitOfWork unitOfWork, IMapper mapper) : IPersonService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<List<GetPersonModel>> GetPersons(string? id)
        {
            List<Person> response = await _unitOfWork.GetRepository<Person>().Entities.Where(g => !g.DeletedTime.HasValue).ToListAsync();
            if(!string.IsNullOrWhiteSpace(id))
            {
                response = response.Where(p => p.Id == id).ToList();
            }
            return _mapper.Map<List<GetPersonModel>>(response);
        }

        public async Task PostPerson(PostPersonModel model)
        {
            Person person = _mapper.Map<Person>(model);
            person.CreatedTime = DateTime.Now;
            await _unitOfWork.GetRepository<Person>().InsertAsync(person);
            await _unitOfWork.SaveAsync();
        }

        public async Task PutPerson(string id, PutPersonModel model)
        {
            Person existedPerson = await _unitOfWork.GetRepository<Person>().GetByIdAsync(id);
            if (existedPerson == null)
            {
                throw new Exception($"Person with ID {id} doesn't exist!");
            }
            _mapper.Map(model, existedPerson);
            existedPerson.LastUpdatedTime = DateTime.Now;
            await _unitOfWork.GetRepository<Person>().UpdateAsync(existedPerson);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeletePerson(string id)
        {
            Person existedPerson = await _unitOfWork.GetRepository<Person>().GetByIdAsync(id);
            if (existedPerson == null)
            {
                throw new Exception($"Person with ID {id} doesn't exist!");
            }
            existedPerson.DeletedTime = DateTime.Now;
            await _unitOfWork.GetRepository<Person>().UpdateAsync(existedPerson);
            await _unitOfWork.SaveAsync();
        }

      
    }
}
