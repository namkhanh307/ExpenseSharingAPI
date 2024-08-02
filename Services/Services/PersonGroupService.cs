using AutoMapper;
using Core.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Repositories.Entities;
using Repositories.IRepositories;
using Repositories.ResponseModel.ExpenseModel;
using Repositories.ResponseModel.GroupModel;
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
        public readonly IAuthService _authService;
        private readonly IHttpContextAccessor _contextAccessor;
        public PersonGroupService(IUnitOfWork unitOfWork, IMapper mapper, IAuthService authService, IHttpContextAccessor contextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _authService = authService;
            _contextAccessor = contextAccessor;
        }

        public async Task<List<GetPersonGroupModel>> GetPersonGroups(string? groupId)
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

        public async Task<List<GetGroupModel>> GetAllGroupsByPersonId(string personId)
        {
            var query = _unitOfWork.GetRepository<PersonGroup>()
                                .Entities.Where(pg => !pg.DeletedTime.HasValue && pg.PersonId.Equals(personId) )
                                .Include(p => p.Group)
                                .Select(p => p.Group)
                                .ToList();

            return _mapper.Map<List<GetGroupModel>>(query);
        }

        public async Task PostPersonGroup(PostPersonGroupModel model)
        {
            var personGroup = _mapper.Map<PersonGroup>(model);
            personGroup.CreatedTime = DateTime.Now;
            _unitOfWork.GetRepository<PersonGroup>().Insert(personGroup);
            _unitOfWork.Save();
        }

        public async Task PutPersonGroup(string groupId, string personId, PutPersonGroupModel model)
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
        public async Task DeletePersonGroup(string groupId, string? personId, bool? wantToOut)
        {
            var currentUserId = Authentication.GetUserIdFromHttpContextAccessor(_contextAccessor);
            Guid.TryParse(currentUserId, out var id);

            var currentPersonGroup = _unitOfWork.GetRepository<PersonGroup>()
                        .Entities.Where(p => p.PersonId.Equals(currentUserId) && p.GroupId.Equals(groupId))
                        .FirstOrDefault()
                        ?? throw new Exception($"");      

            if (wantToOut.Equals(true))
            {
                if (currentPersonGroup.IsAdmin.Equals(true))
                {
                    var existedPersonGroup = _unitOfWork.GetRepository<PersonGroup>().Entities.Where(pg => pg.GroupId == groupId && pg.PersonId == personId).FirstOrDefault()
                                            ?? throw new Exception($"PersonGroup with personID {personId} or groupID {groupId} doesn't exist!");

                    currentPersonGroup.IsAdmin = false;
                    existedPersonGroup.IsAdmin = true;
                }

                currentPersonGroup.DeletedTime = DateTime.Now;
                currentPersonGroup.DeletedBy = currentUserId;
                _unitOfWork.GetRepository<PersonGroup>().Update(currentPersonGroup);
                _unitOfWork.Save();
            }
            else if (currentPersonGroup.IsAdmin.Equals(true))
            {
                var existedPersonGroup = _unitOfWork.GetRepository<PersonGroup>().Entities.Where(pg => pg.GroupId == groupId && pg.PersonId == personId).FirstOrDefault()
                                            ?? throw new Exception($"PersonGroup with personID {personId} or groupID {groupId} doesn't exist!");

                existedPersonGroup.DeletedTime = DateTime.Now;
                existedPersonGroup.DeletedBy = currentUserId;
                _unitOfWork.GetRepository<PersonGroup>().Update(existedPersonGroup);
                _unitOfWork.Save();
            }
            else
            {
                throw new ErrorException(StatusCodes.Status401Unauthorized, ResponseCodeConstants.BADREQUEST, "Xoa khong thanh cong!");
            }
        }
    }
}
