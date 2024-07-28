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
        List<GetFriendModel> GetFriends();
        void PostFriend(PostFriendModel model);
        void DeleteFriend(string id);
    }
}
