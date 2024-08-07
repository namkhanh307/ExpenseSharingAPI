using AutoMapper;
using Core.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Repositories.Entities;
using Repositories.IRepositories;
using Repositories.ResponseModel.FriendRequestModel;
using Services.IServices;
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
            List<FriendRequest> result = await _unitOfWork.GetRepository<FriendRequest>()
                .Entities
                .Where(p => p.SenderId == CurrentUserId)
                .ToListAsync();
            return _mapper.Map<List<GetFriendRequestModel>>(result);
        }

        public async Task PostFriendRequest(string receivedId)
        {
            Friend? existedFriend = await _unitOfWork.GetRepository<Friend>()
                .Entities.FirstOrDefaultAsync(f => f.FriendId == receivedId);

            FriendRequest? existedFriendRequest = await _unitOfWork.GetRepository<FriendRequest>()
                .Entities.FirstOrDefaultAsync(f => f.ReceiverId == receivedId);

            if (existedFriend != null)
            {
                throw new ErrorException(StatusCodes.Status409Conflict, ErrorCode.Conflicted, "Bạn và người dùng này đã là bạn bè!");
            }

            if (existedFriendRequest != null)
            {
                throw new ErrorException(StatusCodes.Status409Conflict, ErrorCode.Conflicted, "Yêu cầu kết bạn đã tồn tại!");
            }

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

        public async Task AcceptFriendRequest(string friendRequestId)
        {
            FriendRequest? friendRequest = await _unitOfWork
                .GetRepository<FriendRequest>()
                .Entities
                .FirstOrDefaultAsync(fr => fr.Id == friendRequestId && fr.Status == "pending");

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
            else
            {
                throw new ErrorException(StatusCodes.Status404NotFound, ErrorCode.NotFound, "Yêu cầu kết bạn không tồn tại hoặc đã bị xử lý!");
            }
        }

        public void DeleteFriendRequest(string friendRequestId)
        {
            FriendRequest? friendRequest = _unitOfWork.GetRepository<FriendRequest>()
                .Entities
                .FirstOrDefault(fr => fr.Id == friendRequestId && fr.Status == "pending");

            if (friendRequest != null)
            {
                _unitOfWork.GetRepository<FriendRequest>().Delete(friendRequest);
                _unitOfWork.Save();
            }
            else
            {
                throw new ErrorException(StatusCodes.Status404NotFound, ErrorCode.NotFound, "Yêu cầu kết bạn không tồn tại hoặc đã bị xử lý!");
            }
        }

        public void RejectFriendRequest(string friendRequestId)
        {
            FriendRequest? friendRequest = _unitOfWork.GetRepository<FriendRequest>()
                .Entities
                .FirstOrDefault(fr => fr.Id == friendRequestId && fr.Status == "pending");

            if (friendRequest != null)
            {
                friendRequest.Status = "rejected";
                friendRequest.DeletedBy = CurrentUserId;
                friendRequest.DeletedTime = DateTime.Now;
                _unitOfWork.GetRepository<FriendRequest>().Update(friendRequest);
                _unitOfWork.Save();
            }
            else
            {
                throw new ErrorException(StatusCodes.Status404NotFound, ErrorCode.NotFound, "Yêu cầu kết bạn không tồn tại hoặc đã bị xử lý!");
            }
        }
    }
}
