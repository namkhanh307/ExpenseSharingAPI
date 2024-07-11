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
        public List<GetPersonGroupModel> GetPersonGroups()
        {
            return _personGroupService.GetPersonGroups();
        }
        [HttpPost]
        public void PostPersonGroup(PostPersonGroupModel model)
        {
            _personGroupService.PostPersonGroup(model);
        }
        [HttpPut]
        public void PutPersonGroup(string id, PutPersonGroupModel model)
        {
            _personGroupService.PutPersonGroup(id, model);
        }
        [HttpDelete]
        public void DeletePersonGroup(string id)
        {
            _personGroupService.DeletePersonGroup(id);
        }
    }
}
