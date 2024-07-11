
namespace Repositories.ResponseModel.PersonGroupModel
{
    public class GetPersonGroupModel
    {
        public string PersonId { get; set; }

        public string GroupId { get; set; }

        public bool? IsAdmin { get; set; }
    }
}
