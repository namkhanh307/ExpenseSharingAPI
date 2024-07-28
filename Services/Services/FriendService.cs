using AutoMapper;
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
    internal class FriendService : IFriendService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public FriendService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public List<GetFriendModel> GetFriends(string id)
        {
            return _mapper.Map<List<GetFriendModel>>(_unitOfWork.GetRepository<Friend>().Entities.Where(g => !g.DeletedTime.HasValue&& g.PersonId==id).ToList());
        }

        public void PostFriend(PostFriendModel model)
        {
            var friend = _mapper.Map<Friend>(model);
            friend.CreatedTime = DateTime.Now;
            _unitOfWork.GetRepository<Friend>().Insert(friend);
            _unitOfWork.Save();
        }

        public void DeleteFriend(string id)
        {
            var existedFriend = _unitOfWork.GetRepository<Friend>().GetById(id);
            if (existedFriend == null)
            {
                throw new Exception($"You don't have friend who id is {id}!");
            }
            existedFriend.DeletedTime = DateTime.Now;
            _unitOfWork.GetRepository<Friend>().Update(existedFriend);
            _unitOfWork.Save();
        }
    }
}
