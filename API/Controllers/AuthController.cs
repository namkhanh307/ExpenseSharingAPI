using Core.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repositories.ResponseModel.AuthModel;
using Repositories.ResponseModel.PersonModel;
using Services.IServices;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpPost("signup")]
        public IActionResult SignUp(PostSignUpModel model)  
        {
            _authService.SignUp(model);
            return Ok(new BaseResponseModel<string>(
                statusCode: StatusCodes.Status200OK,
                code: ResponseCodeConstants.SUCCESS,
                data: "Dang ky thanh cong!"));
        }
        [HttpPost("login")]
        public IActionResult LogIn(PostLogInModel model)
        {
            var result = _authService.LogIn(model);
            return Ok(new BaseResponseModel<GetLogInModel>(
                           statusCode: StatusCodes.Status200OK,
                           code: ResponseCodeConstants.SUCCESS,
                           data: result));
        }
        [HttpGet("getinfo")]
        public IActionResult GetInfo()
        {
            var result = _authService.GetInfo();
            return Ok(new BaseResponseModel<GetPersonModel>(
                statusCode: StatusCodes.Status200OK,
                code: ResponseCodeConstants.SUCCESS,
                data: result));
        }
    }
}
