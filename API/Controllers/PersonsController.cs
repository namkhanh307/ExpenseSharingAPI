using Core.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Repositories.Entities;
using Repositories.ResponseModel.GroupModel;
using Repositories.ResponseModel.PersonModel;
using Services.IServices;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonsController : ControllerBase
    {
        private readonly IPersonService _personService;
        public PersonsController(IPersonService personService)
        {
            _personService = personService;
        }
        [HttpGet]
        public IActionResult GetPersons(string? id)
        {
            var result = _personService.GetPersons(id);
            return Ok(new BaseResponseModel<List<GetPersonModel>>(
               statusCode: StatusCodes.Status200OK,
               code: ResponseCodeConstants.SUCCESS,
               data: result));
        }
        [HttpPost]
        public IActionResult PostPerson(PostPersonModel model)
        {
            _personService.PostPerson(model);
            return Ok(new BaseResponseModel<string>(
               statusCode: StatusCodes.Status200OK,
               code: ResponseCodeConstants.SUCCESS,
               data: "Them thanh vien thanh cong"));
        }
        [HttpPut]
        public IActionResult PutPerson(string id, PutPersonModel model)
        {
            _personService.PutPerson(id, model);
            return Ok(new BaseResponseModel<string>(
               statusCode: StatusCodes.Status200OK,
               code: ResponseCodeConstants.SUCCESS,
               data: "Chinh sua thanh vien thanh cong"));
        }
        [HttpDelete]
        public IActionResult DeletePerson(string id)
        {
            _personService.DeletePerson(id);
            return Ok(new BaseResponseModel<string>(
               statusCode: StatusCodes.Status200OK,
               code: ResponseCodeConstants.SUCCESS,
               data: "Xoa thanh vien thanh cong"));
        }
    }
}
