using Microsoft.AspNetCore.Mvc;
using Repositories.Entities;
using Repositories.ResponseModel.PersonGroupModel;
using Services.IServices;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonGroupsController : ControllerBase
    {
        private readonly IPersonGroupService _personGroupService;
        public PersonGroupsController(IPersonGroupService personGroupService)
        {
            _personGroupService = personGroupService;
        }
        [HttpGet]
        public List<GetPersonGroupModel> GetPersonGroups(string? groupId)
        {
            return _personGroupService.GetPersonGroups(groupId);
        }
        [HttpPost]
        public void PostPersonGroup(PostPersonGroupModel model)
        {
            _personGroupService.PostPersonGroup(model);
        }
        [HttpPut]
        public void PutPersonGroup(string groupId, string personId, PutPersonGroupModel model)
        {
            _personGroupService.PutPersonGroup(groupId, personId, model);
        }
        [HttpDelete]
        public void DeletePersonGroup(string groupId, string personId)
        {
            _personGroupService.DeletePersonGroup(groupId, personId);
        }
    }
}
