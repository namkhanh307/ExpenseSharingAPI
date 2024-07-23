
using Repositories.ResponseModel.PersonModel;

namespace Repositories.ResponseModel.PersonGroupModel
{
    public class GetPersonGroupModel
    {
        public string? GroupId { get; set; }
        public List<GetPersonModel>? Persons { get; set; }
    }
}
