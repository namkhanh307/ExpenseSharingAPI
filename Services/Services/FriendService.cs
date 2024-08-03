using AutoMapper;
using Core.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Repositories.Entities;
using Repositories.IRepositories;
using Repositories.ResponseModel.FriendModel;
using Services.IServices;

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

        public async Task<List<GetFriendModel>> GetFriends()
        {
            var friends = await _unitOfWork.GetRepository<Friend>().Entities
                                    .Where(g => !g.DeletedTime.HasValue &&
                                                (g.PersonId.Equals(currentUserId) || g.FriendId.Equals(currentUserId)))
                                    .Include(f => f.Person)
                                    .Include(f => f.FriendPerson)
                                    .ToListAsync();

            var result = friends.Select(f => new GetFriendModel
            {
                FriendId = f.PersonId.Equals(currentUserId) ? f.FriendId : f.PersonId,
                FriendName = f.PersonId.Equals(currentUserId) ? f.FriendPerson.Name : f.Person.Name
            }).ToList();

            return result;
        }

        public async Task PostFriend(PostFriendModel model)
        {
            var friend = _mapper.Map<Friend>(model);
            friend.PersonId = currentUserId;
            friend.CreatedTime = DateTime.Now;
            await _unitOfWork.GetRepository<Friend>().InsertAsync(friend);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteFriend(string id)
        {
            var existedFriend = await _unitOfWork.GetRepository<Friend>().Entities.Where(g => g.PersonId.Equals(currentUserId) && g.FriendId.Equals(id)).FirstOrDefaultAsync();
            if (existedFriend == null)
            {
                throw new Exception($"You don't have friend who id is {id}!");
            }
            existedFriend.DeletedTime = DateTime.Now;
            existedFriend.DeletedBy = currentUserId;
            await _unitOfWork.GetRepository<Friend>().UpdateAsync(existedFriend);
            await _unitOfWork.SaveAsync();
        }
    }
}
