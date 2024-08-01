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
    public class PersonsController(IPersonService personService) : ControllerBase
    {
        private readonly IPersonService _personService = personService;

        [HttpGet]
        public async Task<IActionResult> GetPersons(string? id)
        {
            List<GetPersonModel> result = await _personService.GetPersons(id);
            return Ok(new BaseResponseModel<List<GetPersonModel>>(
               statusCode: StatusCodes.Status200OK,
               code: ResponseCodeConstants.SUCCESS,
               data: result));
        }
        [HttpPost]
        public async Task<IActionResult> PostPerson(PostPersonModel model)
        {
            await _personService.PostPerson(model);
            return Ok(new BaseResponseModel<string>(
               statusCode: StatusCodes.Status200OK,
               code: ResponseCodeConstants.SUCCESS,
               data: "Person added SUCCESSFULLY!"));
        }
        [HttpPut]
        public async Task<IActionResult> PutPerson(string id, PutPersonModel model)
        {
            await _personService.PutPerson(id, model);
            return Ok(new BaseResponseModel<string>(
               statusCode: StatusCodes.Status200OK,
               code: ResponseCodeConstants.SUCCESS,
               data: "Person modified SUCCESSFULLY!"));
        }
        [HttpDelete]
        public async Task<IActionResult> DeletePerson(string id)
        {
            await _personService.DeletePerson(id);
            return Ok(new BaseResponseModel<string>(
               statusCode: StatusCodes.Status200OK,
               code: ResponseCodeConstants.SUCCESS,
               data: "Person deleted SUCCESSFULLY!"));
        }
    }
}
