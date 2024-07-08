using Repositories.Entities;
using Repositories.ResponseModel.GroupModel;

namespace Repositories.IRepositories
{
    public interface IGroupRepository
    {
        List<Group> GetGroup();
        void PostGroup(PostGroupModel model);
        void PutGroup(string id, PutGroupModel model);
        void DeleteGroup(string id);

    }
}
