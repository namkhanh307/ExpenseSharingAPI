using AutoMapper;
using Core.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Repositories.Entities;
using Repositories.IRepositories;
using Repositories.ResponseModel.FriendModel;
using Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class FriendService : IFriendService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _contextAccessor;
        public FriendService(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _contextAccessor = httpContextAccessor;
        }

        private string currentUserId => Authentication.GetUserIdFromHttpContextAccessor(_contextAccessor);

        public List<GetFriendModel> GetFriends()
        {
            return _mapper.Map<List<GetFriendModel>>(_unitOfWork.GetRepository<Friend>().Entities.Where(g => !g.DeletedTime.HasValue && g.PersonId.Equals(currentUserId)).ToList());
        }

        public void PostFriend(PostFriendModel model)
        {
            var friend = _mapper.Map<Friend>(model);
            friend.PersonId = currentUserId;
            friend.CreatedTime = DateTime.Now;
            _unitOfWork.GetRepository<Friend>().Insert(friend);
            _unitOfWork.Save();
        }

        public void DeleteFriend(string id)
        {
            var existedFriend = _unitOfWork.GetRepository<Friend>().Entities.Where(g => g.PersonId.Equals(currentUserId) && g.FriendId.Equals(id)).FirstOrDefault();
            if (existedFriend == null)
            {
                throw new Exception($"You don't have friend who id is {id}!");
            }
            existedFriend.DeletedTime = DateTime.Now;
            existedFriend.DeletedBy = currentUserId;
            _unitOfWork.GetRepository<Friend>().Update(existedFriend);
            _unitOfWork.Save();
        }
    }
}
