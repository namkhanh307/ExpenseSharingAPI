using Repositories.Entities;
using Repositories.ResponseModel.GroupModel;

namespace Services.IServices
{
    public interface IGroupService
    {
        void DeleteGroup(string id);
        List<GetGroupModel> GetGroups();
        void PostGroup(PostGroupModel model);
        void PutGroup(string id, PutGroupModel model);
    }
}
