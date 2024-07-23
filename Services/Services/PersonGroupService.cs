using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Repositories.Entities;
using Repositories.IRepositories;
using Repositories.ResponseModel.ExpenseModel;
using Repositories.ResponseModel.PersonGroupModel;
using Repositories.ResponseModel.PersonModel;
using Services.IServices;
using System.Collections.Generic;

namespace Services.Services
{
    public class PersonGroupService : IPersonGroupService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public PersonGroupService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public List<GetPersonGroupModel> GetPersonGroups(string? groupId)
        {
            var query = _unitOfWork.GetRepository<PersonGroup>()
                                   .Entities.Where(pg => !pg.DeletedTime.HasValue)
                                   .Include(pg => pg.Person)
                                   .AsQueryable();

            if (!string.IsNullOrWhiteSpace(groupId))
            {
                query = query.Where(pg => pg.GroupId == groupId);
            }

            var groupQuery = query.GroupBy(pg => pg.GroupId).ToList();

            var responseList = groupQuery.Select(g => new GetPersonGroupModel
            {
                GroupId = g.Key,
                Persons = g.Select(pg => _mapper.Map<GetPersonModel>(pg.Person)).ToList()
            }).ToList();

            return responseList;
        }

        public void PostPersonGroup(PostPersonGroupModel model)
        {
            var personGroup = _mapper.Map<PersonGroup>(model);
            personGroup.CreatedTime = DateTime.Now;
            _unitOfWork.GetRepository<PersonGroup>().Insert(personGroup);
            _unitOfWork.Save();
        }

        public void PutPersonGroup(string groupId, string personId, PutPersonGroupModel model)
        {
            var existedPersonGroup = _unitOfWork.GetRepository<PersonGroup>().Entities.Where(pg => pg.GroupId == groupId && pg.PersonId == personId).FirstOrDefault();
            if (existedPersonGroup == null)
            {
                throw new Exception($"PersonGroup with personID {personId} or groupID {groupId} doesn't exist!");
            }
            _mapper.Map(model, existedPersonGroup);
            existedPersonGroup.LastUpdatedTime = DateTime.Now;
            _unitOfWork.GetRepository<PersonGroup>().Update(existedPersonGroup);
            _unitOfWork.Save();
        }
        public void DeletePersonGroup(string groupId, string personId)
        {
            var existedPersonGroup = _unitOfWork.GetRepository<PersonGroup>().Entities.Where(pg => pg.GroupId == groupId && pg.PersonId == personId).FirstOrDefault();
            if (existedPersonGroup == null)
            {
                throw new Exception($"PersonGroup with personID {personId} or groupID {groupId} doesn't exist!");
            }
            existedPersonGroup.DeletedTime = DateTime.Now;
            _unitOfWork.GetRepository<PersonGroup>().Update(existedPersonGroup);
            _unitOfWork.Save();
        }
    }
}
