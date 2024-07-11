using Repositories.Entities;
using Repositories.ResponseModel.PersonGroupModel;

namespace Services.IServices
{
    public interface IPersonGroupService
    {
        void DeletePersonGroup(string id);
        List<GetPersonGroupModel> GetPersonGroups();
        void PostPersonGroup(PostPersonGroupModel model);
        void PutPersonGroup(string id, PutPersonGroupModel model);
    }
}
