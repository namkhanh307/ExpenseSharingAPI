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
    public class FriendRequestService : IFriendRequestService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly ILogger<FriendRequestService> _logger;

        public FriendRequestService(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor contextAccessor, ILogger<FriendRequestService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _contextAccessor = contextAccessor;
            _logger = logger;
        }

        private string CurrentUserId => Authentication.GetUserIdFromHttpContextAccessor(_contextAccessor);

        public List<GetFriendRequestModel> GetFriendRequest()
        {
            var result = _unitOfWork.GetRepository<FriendRequest>()
                .Entities
                .Where(p => p.SenderId == CurrentUserId)
                .ToList();
            return _mapper.Map<List<GetFriendRequestModel>>(result);
        }

        public void PostFriendRequest(string receivedId)
        {
            var newFriendRequest = new FriendRequest
            {
                SenderId = CurrentUserId,
                ReceiverId = receivedId,
                Status = "pending",
                CreatedBy = CurrentUserId,
            };

            _unitOfWork.GetRepository<FriendRequest>().Insert(newFriendRequest);
            _unitOfWork.Save();
        }

        public void AcceptFriendRequest(string id)
        {
            var friendRequest = _unitOfWork.GetRepository<FriendRequest>()
                .Entities
                .FirstOrDefault(fr => fr.Id == id && fr.Status == "pending");

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
            var friendRequest = _unitOfWork.GetRepository<FriendRequest>()
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
            var friendRequest = _unitOfWork.GetRepository<FriendRequest>()
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
