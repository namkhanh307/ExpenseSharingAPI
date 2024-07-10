using Repositories.Entities;
using Repositories.ResponseModel.PersonModel;

namespace Services.IServices
{
    public interface IPersonService
    {
        void DeletePerson(string id);
        List<Person> GetPersons();
        void PostPerson(PostPersonModel model);
        void PutPerson(string id, PutPersonModel model);
    }
}
