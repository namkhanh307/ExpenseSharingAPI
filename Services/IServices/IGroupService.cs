using Repositories.Entities;
using Repositories.ResponseModel.GroupModel;

namespace Services.IServices
{
    public interface IGroupService
    {
        Task<List<GetGroupModel>> GetGroups();
        Task PostGroup(PostGroupModel model);
        Task PutGroup(string id, PutGroupModel model);
        Task DeleteGroup(string id);

    }
}
