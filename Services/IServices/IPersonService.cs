using Repositories.Entities;
using Repositories.ResponseModel.PersonModel;

namespace Services.IServices
{
    public interface IPersonService
    {
        void DeletePerson(string id);
        List<GetPersonModel> GetPersons(string? id);
        void PostPerson(PostPersonModel model);
        void PutPerson(string id, PutPersonModel model);
    }
}
