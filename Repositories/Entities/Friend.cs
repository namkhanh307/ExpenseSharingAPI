﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Entities
{
    public class Friend
    {
        public string PersonId { get; set; }
        public string FriendId { get; set; }
        public Person Person { get; set; }
        public Person FriendPerson { get; set; }
        public string? CreatedBy { get; set; }
        public string? LastUpdatedBy { get; set; }
        public string? DeletedBy { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime LastUpdatedTime { get; set; }
        public DateTime? DeletedTime { get; set; }
    }
}
