using Repositories.Entities;
using Repositories.IRepositories;
using Repositories.ResponseModel.GroupModel;

namespace Repositories.Repositories
{
    public class GroupRepository : GenericRepository<Group>, IGroupRepository
    {
        private readonly ExpenseSharingContext _dbContext;
        public GroupRepository(ExpenseSharingContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        //public List<Group> GetGroup()
        //{
        //    return _context.Groups.ToList();
        //}

        //public void PostGroup(PostGroupModel model)
        //{
        //    var group = new Group()
        //    {
        //        Id = Guid.NewGuid().ToString(),
        //        Name = model.Name,
        //        Type = model.Type,
        //        Size = model.Size,      
        //    };
        //    _context.Groups.Add(group);
        //    _context.SaveChanges();
        //}

        //public void PutGroup(string id, PutGroupModel model)
        //{
        //    var group = _context.Groups.FirstOrDefault(g => g.Id == id);
        //    if(group != null)
        //    {
        //        group.Name = model.Name;
        //        group.Size = model.Size;
        //        group.Type = model.Type;
        //        _context.Groups.Update(group);
        //        _context.SaveChanges();
        //    }
        //    else
        //    {
        //        throw new Exception($"Group with{id} doesn't exist");
        //    }

        //}
        //public void DeleteGroup(string id)
        //{
        //    var group = _context.Groups.FirstOrDefault(g => g.Id == id);
        //    _context.Groups.Remove(group);
        //    _context.SaveChanges();
        //}
    }
}
