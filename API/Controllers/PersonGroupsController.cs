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
        [HttpGet]
        public IActionResult GetPersonGroups(string? groupId)
        {
            var result = _personGroupService.GetPersonGroups(groupId);
            return Ok(new BaseResponseModel<List<GetPersonGroupModel>>(
               statusCode: StatusCodes.Status200OK,
               code: ResponseCodeConstants.SUCCESS,
               data: result));
        }
        [HttpPost]
        public IActionResult PostPersonGroup(PostPersonGroupModel model)
        {
            _personGroupService.PostPersonGroup(model);
            return Ok(new BaseResponseModel<string>(
               statusCode: StatusCodes.Status200OK,
               code: ResponseCodeConstants.SUCCESS,
               data: "Them thanh vien vao nhom thanh cong"));
        }
        [HttpPut]
        public IActionResult PutPersonGroup(string groupId, string personId, PutPersonGroupModel model)
        {
            _personGroupService.PutPersonGroup(groupId, personId, model);
            return Ok(new BaseResponseModel<string>(
               statusCode: StatusCodes.Status200OK,
               code: ResponseCodeConstants.SUCCESS,
               data: "Chinh sua thanh vien trong nhom thanh cong"));
        }
        [HttpDelete]
        public IActionResult DeletePersonGroup(string groupId, string personId)
        {
            _personGroupService.DeletePersonGroup(groupId, personId);
            return Ok(new BaseResponseModel<string>(
               statusCode: StatusCodes.Status200OK,
               code: ResponseCodeConstants.SUCCESS,
               data: "Xoa thanh vien khoi nhom thanh cong"));
        }
    }
}
