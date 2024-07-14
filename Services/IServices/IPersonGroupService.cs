using Repositories.Entities;
using Repositories.ResponseModel.PersonGroupModel;

namespace Services.IServices
{
    public interface IPersonGroupService
    {
        void DeletePersonGroup(string groupId, string personId);
        List<GetPersonGroupModel> GetPersonGroups(string? groupId);
        void PostPersonGroup(PostPersonGroupModel model);
        void PutPersonGroup(string groupId, string personId, PutPersonGroupModel model);
    }
}
