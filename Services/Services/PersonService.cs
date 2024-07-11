﻿using AutoMapper;
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
        public List<Person> GetPersons()
        {
            return _unitOfWork.GetRepository<Person>().Entities.Where(g => !g.DeletedTime.HasValue).ToList();
        }

        public void PostPerson(PostPersonModel model)
        {
            var person = _mapper.Map<Person>(model);
            person.CreatedTime = DateTime.Now;
            _unitOfWork.GetRepository<Person>().Insert(person);
            _unitOfWork.Save();
        }

        public void PutPerson(string id, PutPersonModel model)
        {
            var existedPerson = _unitOfWork.GetRepository<Person>().GetById(id);
            if (existedPerson == null)
            {
                throw new Exception($"Group with ID {id} doesn't exist!");
            }
            _mapper.Map(model, existedPerson);
            existedPerson.LastUpdatedTime = DateTime.Now;
            _unitOfWork.GetRepository<Person>().Update(existedPerson);
            _unitOfWork.Save();
        }

        public void DeletePerson(string id)
        {
            var existedPerson = _unitOfWork.GetRepository<Person>().GetById(id);
            if (existedPerson == null)
            {
                throw new Exception($"Group with ID {id} doesn't exist!");
            }
            existedPerson.DeletedTime = DateTime.Now;
            _unitOfWork.GetRepository<Person>().Update(existedPerson);
            _unitOfWork.Save();
        }

      
    }
}
