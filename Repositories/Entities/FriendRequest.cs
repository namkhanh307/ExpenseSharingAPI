using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Entities
{
    public partial class FriendRequest : BaseEntity
    {
        public string Id { get; set; }
        public string SenderId { get; set; }
        public string ReceiverId { get; set; }
        public DateTime RequestDate { get; set; }
        public string Status { get; set; }
        public virtual Person Sender { get; set; }
        public virtual Person Receiver { get; set; }
    }
}
