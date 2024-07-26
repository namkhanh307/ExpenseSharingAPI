using Repositories.Entities;
using Repositories.ResponseModel.GroupModel;
using Repositories.ResponseModel.PersonGroupModel;

namespace Services.IServices
{
    public interface IPersonGroupService
    {
        void DeletePersonGroup(string groupId, string? personId, bool? wantToOut);
        List<GetPersonGroupModel> GetPersonGroups(string? groupId);
        public List<GetGroupModel> GetAllGroupsByPersonId(string personId);
        void PostPersonGroup(PostPersonGroupModel model);
        void PutPersonGroup(string groupId, string personId, PutPersonGroupModel model);
    }
}
