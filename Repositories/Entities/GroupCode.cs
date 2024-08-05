using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Entities
{
    public class GroupCode : BaseEntity
    {
        public string AccessCode { get; set; } = string.Empty;
        public string GroupId { get; set; } = string.Empty;
        public DateTime ExpiredTime { get; set; }
        public virtual Group Group { get; set; } = new Group();
    }
}
