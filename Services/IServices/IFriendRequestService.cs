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
        public List<GetFriendRequestModel> GetFriendRequest();
        public void PostFriendRequest(string receivedId);
        public void AcceptFriendRequest(string id);
        public void RejectFriendRequest(string id);
    }
}
