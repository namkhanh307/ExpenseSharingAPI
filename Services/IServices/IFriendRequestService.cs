using Repositories.ResponseModel.FriendRequestModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.IServices
{
    public interface IFriendRequestService
    {
        public Task<List<GetFriendRequestModel>> GetFriendRequest();
        public Task PostFriendRequest(string receivedId);
        public Task AcceptFriendRequest(string id);
        public void RejectFriendRequest(string id);
    }
}
