﻿using AutoMapper;
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
using System.IdentityModel.Tokens.Jwt;

namespace Services.Services
{
    public class PersonGroupService : IPersonGroupService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IAuthService _authService;
        private readonly IHttpContextAccessor _contextAccessor;
        private string CurrentUserId => Authentication.GetUserIdFromHttpContextAccessor(_contextAccessor);
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

            var groupQuery = await query.GroupBy(pg => pg.GroupId).ToListAsync();

            var responseList = groupQuery.Select(g => new GetPersonGroupModel
            {
                GroupId = g.Key,
                Persons = g.Select(pg => _mapper.Map<GetPersonModel>(pg.Person)).ToList()
            }).ToList();

            return responseList;
        }

        public async Task<List<GetGroupModel>> GetAllGroupsByPersonId(string personId)
        {
            List<Group> query = await _unitOfWork.GetRepository<PersonGroup>()
                                .Entities.Where(pg => !pg.DeletedTime.HasValue && pg.PersonId.Equals(personId))
                                .Include(p => p.Group)
                                .Select(p => p.Group)
                                .ToListAsync();

            return _mapper.Map<List<GetGroupModel>>(query);
        }

        public async Task PostPersonGroup(PostPersonGroupModel model)
        {
            var personGroup = _mapper.Map<PersonGroup>(model);
            personGroup.CreatedTime = DateTime.Now;
            await _unitOfWork.GetRepository<PersonGroup>().InsertAsync(personGroup);
            await _unitOfWork.SaveAsync();
        }

        private static string GenerateAccessCode(int length = 10)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public async Task<string> GenerateAccessCode(string groupId)
        {
            //Delete all codes that are expired
            GroupCode? expiredCode = await _unitOfWork
                .GetRepository<GroupCode>()
                .Entities.FirstOrDefaultAsync(c => c.ExpiredTime <= DateTime.Now && c.GroupId == groupId);

            if (expiredCode != null)
            {
                await _unitOfWork.GetRepository<GroupCode>().DeleteAsync(expiredCode.Id);
            }

            GroupCode? checkCode = await _unitOfWork
                .GetRepository<GroupCode>()
                .Entities
                .FirstOrDefaultAsync(c => c.GroupId == groupId && c.ExpiredTime > DateTime.Now);

            if (checkCode != null)
            {
                return checkCode.AccessCode;
            }

            var accessCode = GenerateAccessCode();
            var newGroupCode = new GroupCode()
            {
                GroupId = groupId,
                AccessCode = accessCode,
                ExpiredTime = DateTime.Now.AddMinutes(5),
            };
            await _unitOfWork.GetRepository<GroupCode>().InsertAsync(newGroupCode);
            await _unitOfWork.SaveAsync();

            return accessCode;
        }

        public async Task JoinGroup(string groupId, string accessCode) 
        {
            var personIdList = await _unitOfWork.GetRepository<PersonGroup>()
                                                .Entities
                                                .Where(g => g.GroupId == groupId)
                                                .Select(g => g.PersonId).ToListAsync();
            if (personIdList.Contains(CurrentUserId))
            {
                throw new Exception("You're already joined this group!");
            }

            var newPersonGroup = new PostPersonGroupModel()
            {
                PersonId = CurrentUserId,
                GroupId = groupId,
                IsAdmin = false,
            };
            await PostPersonGroup(newPersonGroup);
        }

        public async Task PutPersonGroup(string groupId, string personId, PutPersonGroupModel model)
        {
            var existedPersonGroup = await _unitOfWork.GetRepository<PersonGroup>().Entities.Where(pg => pg.GroupId == groupId && pg.PersonId == personId).FirstOrDefaultAsync();
            if (existedPersonGroup == null)
            {
                throw new Exception($"PersonGroup with personID {personId} or groupID {groupId} doesn't exist!");
            }
            _mapper.Map(model, existedPersonGroup);
            existedPersonGroup.LastUpdatedTime = DateTime.Now;
            await _unitOfWork.GetRepository<PersonGroup>().UpdateAsync(existedPersonGroup);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeletePersonGroup(string groupId, string? personId, bool? wantToOut)
        {
            var currentPersonGroup = await _unitOfWork.GetRepository<PersonGroup>()
                                                    .Entities.Where(p => p.PersonId.Equals(CurrentUserId) && p.GroupId.Equals(groupId))
                                                    .FirstOrDefaultAsync()
                                                    ?? throw new Exception($"");      

            if (wantToOut.Equals(true))
            {
                if (currentPersonGroup.IsAdmin.Equals(true))
                {
                    var existedPersonGroup = await _unitOfWork
                        .GetRepository<PersonGroup>()
                        .Entities.Where(pg => pg.GroupId == groupId && pg.PersonId == personId)
                        .FirstOrDefaultAsync()
                        ?? throw new Exception($"PersonGroup with personID {personId} or groupID {groupId} doesn't exist!");

                    currentPersonGroup.IsAdmin = false;
                    existedPersonGroup.IsAdmin = true;
                }

                currentPersonGroup.DeletedTime = DateTime.Now;
                currentPersonGroup.DeletedBy = CurrentUserId;
                await _unitOfWork.GetRepository<PersonGroup>().UpdateAsync(currentPersonGroup);
                await _unitOfWork.SaveAsync();
            }
            else if (currentPersonGroup.IsAdmin.Equals(true))
            {
                var existedPersonGroup = await _unitOfWork
                    .GetRepository<PersonGroup>().Entities
                    .Where(pg => pg.GroupId == groupId && pg.PersonId == personId).FirstOrDefaultAsync()
                    ?? throw new Exception($"PersonGroup with personID {personId} or groupID {groupId} doesn't exist!");

                existedPersonGroup.DeletedTime = DateTime.Now;
                existedPersonGroup.DeletedBy = CurrentUserId;
                await _unitOfWork.GetRepository<PersonGroup>().UpdateAsync(existedPersonGroup);
                await _unitOfWork.SaveAsync();
            }
            else
            {
                throw new Exception("PersonGroup deleted fail!");
            }
        }
    }
}
