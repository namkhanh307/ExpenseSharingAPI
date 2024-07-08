using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repositories.Entities;
using Repositories.ResponseModel.GroupModel;
using Services.IServices;
using Services.Services;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupsController : ControllerBase
    {
        private readonly IGroupService _groupService;
        public GroupsController(IGroupService groupService)
        {
            _groupService = groupService;
        }
        [HttpGet]
        public List<Group> GetGroups()
        {
            return _groupService.GetGroup();
        }
        [HttpPost]
        public void PostGroup(PostGroupModel model)
        {
            _groupService.PostGroup(model);
        }
        [HttpPut]
        public void PutGroup(string id, PutGroupModel model)
        {
            _groupService.PutGroup(id, model);
        }
        [HttpDelete]
        public void DeleteGroup(string id)
        {
            _groupService.DeleteGroup(id);
        }
    }
}
