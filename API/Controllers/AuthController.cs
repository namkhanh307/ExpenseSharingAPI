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
    public class AuthController(IAuthService authService) : ControllerBase
    {
        public readonly IAuthService _authService = authService;

        [HttpPost("SignUp")]
        public async Task<IActionResult> SignUp(PostSignUpModel model)  
        {
            await _authService.SignUp(model);
            return Ok(new BaseResponseModel<string>(
                statusCode: StatusCodes.Status200OK,
                code: ResponseCodeConstants.SUCCESS,
                data: "Đăng ký thành công"));
        }
        [HttpPost("LogIn")]
        public async Task<IActionResult> LogIn(PostLogInModel model)
        {
            var result = await _authService.LogIn(model);
            return Ok(new BaseResponseModel<GetLogInModel>(
                           statusCode: StatusCodes.Status200OK,
                           code: ResponseCodeConstants.SUCCESS,
                           data: result));
        }
        [HttpGet("GetInfo")]
        public async Task<IActionResult> GetInfo()
        {
            var result = await _authService.GetInfo();
            return Ok(new BaseResponseModel<GetPersonModel>(
                statusCode: StatusCodes.Status200OK,
                code: ResponseCodeConstants.SUCCESS,
                data: result));
        }
    }
}
