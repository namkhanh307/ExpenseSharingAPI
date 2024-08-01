using AutoMapper;
using Repositories.IRepositories;
using Repositories.ResponseModel.FriendModel;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.IServices
{
    public interface IFriendService
    {
        Task<List<GetFriendModel>> GetFriends();
        Task PostFriend(PostFriendModel model);
        Task DeleteFriend(string id);
    }
}
