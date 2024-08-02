using Repositories.Entities;
using Repositories.ResponseModel.PersonModel;

namespace Services.IServices
{
    public interface IPersonService
    {
        Task DeletePerson(string id);
        Task<List<GetPersonModel>> GetPersons(string? id);
        Task PostPerson(PostPersonModel model);
        Task PutPerson(string id, PutPersonModel model);
    }
}
