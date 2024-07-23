using Core.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Repositories.Entities;
using Repositories.ResponseModel.AuthModel;
using Repositories.ResponseModel.GroupModel;
using Services.IServices;

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
        public IActionResult GetGroups()
        {
            var result = _groupService.GetGroups();
            return Ok(new BaseResponseModel<List<GetGroupModel>>(
               statusCode: StatusCodes.Status200OK,
               code: ResponseCodeConstants.SUCCESS,
               data: result));
        }
        [HttpPost]
        public IActionResult PostGroup(PostGroupModel model)
        {
            _groupService.PostGroup(model);
            return Ok(new BaseResponseModel<string>(
               statusCode: StatusCodes.Status200OK,
               code: ResponseCodeConstants.SUCCESS,
               data: "Tao nhom thanh cong"));
        }
        [HttpPut]
        public IActionResult PutGroup(string id, PutGroupModel model)
        {
            _groupService.PutGroup(id, model);
            return Ok(new BaseResponseModel<string>(
               statusCode: StatusCodes.Status200OK,
               code: ResponseCodeConstants.SUCCESS,
               data: "Chinh sua nhom thanh cong"));
        }
        [HttpDelete]
        public IActionResult DeleteGroup(string id)
        {
            _groupService.DeleteGroup(id);
            return Ok(new BaseResponseModel<string>(
               statusCode: StatusCodes.Status200OK,
               code: ResponseCodeConstants.SUCCESS,
               data: "Xoa nhom thanh cong"));
        }
    }
}
