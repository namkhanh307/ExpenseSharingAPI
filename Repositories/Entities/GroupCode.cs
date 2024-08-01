using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Entities
{
    public class GroupCode
    {
        public string Id { get; set; }
        public string accessCode { get; set; }
        public string groupId { get; set; }
        public DateTime expiredTime { get; set; }
        public virtual Group Group { get; set; }
    }
}
