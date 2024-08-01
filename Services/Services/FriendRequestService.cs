using AutoMapper;
using Core.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Repositories.Entities;
using Repositories.IRepositories;
using Repositories.ResponseModel.FriendRequestModel;
using Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Services
{
    public class FriendRequestService(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor contextAccessor) : IFriendRequestService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;
        private readonly IHttpContextAccessor _contextAccessor = contextAccessor;

        private string CurrentUserId => Authentication.GetUserIdFromHttpContextAccessor(_contextAccessor);

        public async Task<List<GetFriendRequestModel>> GetFriendRequest()
        {
            IEnumerable<FriendRequest> result = await _unitOfWork.GetRepository<FriendRequest>()
                                            .Entities
                                            .Where(p => p.SenderId == CurrentUserId)
                                            .ToListAsync();
            return _mapper.Map<List<GetFriendRequestModel>>(result);
        }

        public async Task PostFriendRequest(string receivedId)
        {
            var newFriendRequest = new FriendRequest
            {
                SenderId = CurrentUserId,
                ReceiverId = receivedId,
                Status = "pending",
                CreatedBy = CurrentUserId,
            };

            await _unitOfWork.GetRepository<FriendRequest>().InsertAsync(newFriendRequest);
            await _unitOfWork.SaveAsync();
        }

        public async Task AcceptFriendRequest(string id)
        {
            FriendRequest? friendRequest = await _unitOfWork
                .GetRepository<FriendRequest>()
                .Entities
                .FirstOrDefaultAsync(fr => fr.Id == id && fr.Status == "pending");

            if (friendRequest != null)
            {
                friendRequest.Status = "accepted";
                _unitOfWork.GetRepository<FriendRequest>().Update(friendRequest);

                var newFriend = new Friend
                {
                    PersonId = CurrentUserId,
                    FriendId = friendRequest.ReceiverId,
                    CreatedBy = CurrentUserId,
                    CreatedTime = DateTime.Now,
                };

                _unitOfWork.GetRepository<Friend>().Insert(newFriend);
                _unitOfWork.Save();
            }
        }

        public void DeleteFriendRequest(string id)
        {
            FriendRequest? friendRequest = _unitOfWork.GetRepository<FriendRequest>()
                .Entities
                .FirstOrDefault(fr => fr.Id == id && fr.Status == "pending");

            if (friendRequest != null)
            {
                _unitOfWork.GetRepository<FriendRequest>().Delete(friendRequest);
                _unitOfWork.Save();
            }
        }

        public void RejectFriendRequest(string id)
        {
            FriendRequest? friendRequest = _unitOfWork.GetRepository<FriendRequest>()
                .Entities
                .FirstOrDefault(fr => fr.Id == id && fr.Status == "pending");

            if (friendRequest != null)
            {
                friendRequest.Status = "rejected";
                friendRequest.DeletedBy = CurrentUserId;
                friendRequest.DeletedTime = DateTime.Now;
                _unitOfWork.GetRepository<FriendRequest>().Update(friendRequest);
                _unitOfWork.Save();
            }
        }
    }
}
