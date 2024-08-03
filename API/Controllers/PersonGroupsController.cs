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
    public class PersonGroupsController(IPersonGroupService personGroupService) : ControllerBase
    {
        private readonly IPersonGroupService _personGroupService = personGroupService;

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
               data: "Added person to group SUCCESSFULLY!"));
        }
        [HttpPost("GetAccessCode")]
        public async Task<IActionResult> GetAccessCode(string groupId)
        {
            var accessCode = await _personGroupService.GenerateAccessCode(groupId);
            return Ok(new BaseResponseModel<string>(
               statusCode: StatusCodes.Status200OK,
               code: ResponseCodeConstants.SUCCESS,
               data: accessCode));
        }
        [HttpPost("JoinGroup")]
        public async Task<IActionResult> JoinGroup(string groupId, string accessCode)
        {
            await _personGroupService.JoinGroup(groupId, accessCode);
            return Ok(new BaseResponseModel<string>(
               statusCode: StatusCodes.Status200OK,
               code: ResponseCodeConstants.SUCCESS,
               data: $"Join group {groupId} SUCCESSFULLY!"));
        }
        [HttpPut]
        public async Task<IActionResult> PutPersonGroup(string groupId, string personId, PutPersonGroupModel model)
        {
            await _personGroupService.PutPersonGroup(groupId, personId, model);
            return Ok(new BaseResponseModel<string>(
               statusCode: StatusCodes.Status200OK,
               code: ResponseCodeConstants.SUCCESS,
               data: "PersonGroup modified successfully!"));
        }
        [HttpDelete]
        public async Task<IActionResult> DeletePersonGroup(string groupId, string? personId, bool? wantToOut)
        {
            await _personGroupService.DeletePersonGroup(groupId, personId, wantToOut);
            return Ok(new BaseResponseModel<string>(
               statusCode: StatusCodes.Status200OK,
               code: ResponseCodeConstants.SUCCESS,
               data: "PersonGroup deleted successfully!"));
        }
    }
}
