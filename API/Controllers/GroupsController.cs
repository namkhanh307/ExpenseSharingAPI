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
    public class GroupsController(IGroupService groupService) : ControllerBase
    {
        private readonly IGroupService _groupService = groupService;

        [HttpGet]
        public async Task<IActionResult> GetGroups()
        {
            var result = await _groupService.GetGroups();
            return Ok(new BaseResponseModel<List<GetGroupModel>>(
               statusCode: StatusCodes.Status200OK,
               code: ResponseCodeConstants.SUCCESS,
               data: result));
        }
        [HttpPost]
        public async Task<IActionResult> PostGroup(PostGroupModel model)
        {
            await _groupService.PostGroup(model);
            return Ok(new BaseResponseModel<string>(
               statusCode: StatusCodes.Status200OK,
               code: ResponseCodeConstants.SUCCESS,
               data: "Tạo nhóm thành công"));
        }
        [HttpPut]
        public async Task<IActionResult> PutGroup(string id, PutGroupModel model)
        {
            await _groupService.PutGroup(id, model);
            return Ok(new BaseResponseModel<string>(
               statusCode: StatusCodes.Status200OK,
               code: ResponseCodeConstants.SUCCESS,
               data: "Chỉnh sửa nhóm thành công"));
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteGroup(string id)
        {
            await _groupService.DeleteGroup(id);
            return Ok(new BaseResponseModel<string>(
               statusCode: StatusCodes.Status200OK,
               code: ResponseCodeConstants.SUCCESS,
               data: "Xóa nhóm thành công"));
        }
    }
}
