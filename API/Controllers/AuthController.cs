using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repositories.ResponseModel.AuthModel;
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
            return Ok();
        }
        [HttpPost("login")]
        public IActionResult LogIn(PostLogInModel model)
        {
            var result = _authService.LogIn(model);
            return Ok(result);
        }
        [HttpPost("getinfo")]
        public IActionResult GetInfo()
        {
            var result = _authService.GetInfo();
            return Ok(result);
        }
    }
}
