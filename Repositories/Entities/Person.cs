using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Repositories.Entities;

public partial class Person : BaseEntity
{
    public string? Name { get; set; }

    public string? Phone { get; set; }

    public string? Password { get; set; }
    [JsonIgnore]
    public virtual ICollection<PersonExpense> PersonExpenses { get; set; } = new List<PersonExpense>();
    [JsonIgnore]
    public virtual ICollection<PersonGroup> PersonGroups { get; set; } = new List<PersonGroup>();

    public virtual ICollection<Record> Records { get; set; } = new List<Record>();
    public virtual ICollection<Friend> Friends { get; set; } = new List<Friend>();
    public virtual ICollection<Friend> FriendOf { get; set; } = new List<Friend>();
    public virtual ICollection<FriendRequest> FriendRequestsSent { get; set; } = new List<FriendRequest>();
    public virtual ICollection<FriendRequest> FriendRequestsReceived { get; set; } = new List<FriendRequest>();
}
