using Repositories.Entities;
using Repositories.ResponseModel.GroupModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.IServices
{
    public interface IGroupService
    {
        void DeleteGroup(string id);
        List<Group> GetGroup();
        void PostGroup(PostGroupModel model);
        void PutGroup(string id, PutGroupModel model);
    }
}
