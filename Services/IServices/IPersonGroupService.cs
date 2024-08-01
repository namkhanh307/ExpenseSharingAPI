using Repositories.Entities;
using Repositories.ResponseModel.GroupModel;
using Repositories.ResponseModel.PersonGroupModel;

namespace Services.IServices
{
    public interface IPersonGroupService
    {
        Task DeletePersonGroup(string groupId, string? personId, bool? wantToOut);
        Task<List<GetPersonGroupModel>> GetPersonGroups(string? groupId);
        Task<List<GetGroupModel>> GetAllGroupsByPersonId(string personId);
        Task<string> GenerateAccessCode(string groupId);
        Task JoinGroup(string groupId, string accessCode);
        Task PostPersonGroup(PostPersonGroupModel model);
        Task PutPersonGroup(string groupId, string personId, PutPersonGroupModel model);
    }
}
