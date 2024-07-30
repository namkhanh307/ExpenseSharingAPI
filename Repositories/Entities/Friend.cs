using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Entities
{
    public partial class Friend : BaseEntity
    {
        public string PersonId { get; set; }
        public string FriendId { get; set; }
        public virtual Person Person { get; set; }
        public virtual Person FriendPerson { get; set; }
    }
}
