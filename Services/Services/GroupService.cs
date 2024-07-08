using Repositories.Entities;
using Repositories.IRepositories;
using Repositories.ResponseModel.GroupModel;
using Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class GroupService : IGroupService
    {
        private IGroupRepository _repo;
        public GroupService(IGroupRepository repo)
        {
            _repo = repo;
        }

        public List<Group> GetGroup()
        {
            return _repo.GetGroup();
        }

        public void PostGroup(PostGroupModel model)
        {
            _repo.PostGroup(model);
        }

        public void PutGroup(string id, PutGroupModel model)
        {
            _repo.PutGroup(id, model);
        }
        public void DeleteGroup(string id)
        {
            _repo.DeleteGroup(id);
        }
    }
}
