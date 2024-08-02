using Core.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Repositories.Entities;
using Repositories.ResponseModel.GroupModel;
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
        [HttpGet("GetPersonGroups")]
        public async Task<IActionResult> GetPersonGroups(string? groupId)
        {
            var result = await _personGroupService.GetPersonGroups(groupId);
            return Ok(new BaseResponseModel<List<GetPersonGroupModel>>(
               statusCode: StatusCodes.Status200OK,
               code: ResponseCodeConstants.SUCCESS,
               data: result));
        }
        [HttpGet("GetAllGroupsByPersonId")]
        public async Task<IActionResult> GetAllGroupsByPersonId(string personId)
        {
            var result = await _personGroupService.GetAllGroupsByPersonId(personId);
            return Ok(new BaseResponseModel<List<GetGroupModel>>(
               statusCode: StatusCodes.Status200OK,
               code: ResponseCodeConstants.SUCCESS,
               data: result));
        }
        [HttpPost]
        public async Task<IActionResult> PostPersonGroup(PostPersonGroupModel model)
        {
            await _personGroupService.PostPersonGroup(model);
            return Ok(new BaseResponseModel<string>(
               statusCode: StatusCodes.Status200OK,
               code: ResponseCodeConstants.SUCCESS,
               data: "Thêm thành viên vào nhóm thành công"));
        }
        //[HttpPut]
        //public async Task<IActionResult> PutPersonGroup(string groupId, string personId, PutPersonGroupModel model)
        //{
        //    await _personGroupService.PutPersonGroup(groupId, personId, model);
        //    return Ok(new BaseResponseModel<string>(
        //       statusCode: StatusCodes.Status200OK,
        //       code: ResponseCodeConstants.SUCCESS,
        //       data: "Chỉnh sửa chi tiêu thành công"));
        //}
        [HttpDelete]
        public async Task<IActionResult> DeletePersonGroup(string groupId, string? personId, bool? wantToOut)
        {
            await _personGroupService.DeletePersonGroup(groupId, personId, wantToOut);
            return Ok(new BaseResponseModel<string>(
               statusCode: StatusCodes.Status200OK,
               code: ResponseCodeConstants.SUCCESS,
               data: "Xóa thành viên khỏi nhóm thành công"));
        }
    }
}
