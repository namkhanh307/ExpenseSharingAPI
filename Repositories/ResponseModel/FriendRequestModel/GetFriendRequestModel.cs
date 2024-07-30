using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.ResponseModel.FriendRequestModel
{
    public class GetFriendRequestModel
    {
        public string Id { get; set; }
        public string senderId { get; set; }
        public string receiverId { get; set; }
        public string status { get; set; }
    }
}
