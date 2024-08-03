using Core.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Repositories.ResponseModel.FriendModel;
using Services.IServices;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FriendsController(IFriendService friendService) : ControllerBase
    {
        private readonly IFriendService _friendService = friendService;

        [HttpGet] 
        public async Task<IActionResult> GetFriends()
        {
            var result = await _friendService.GetFriends();
            return Ok(new BaseResponseModel<List<GetFriendModel>>(
                statusCode: StatusCodes.Status200OK,
                code: ResponseCodeConstants.SUCCESS,
                data: result));
        }
        [HttpPost]
        public async Task<IActionResult> PostFriend(PostFriendModel model)
        {
            await _friendService.PostFriend(model);
            return Ok(new BaseResponseModel<string>(
                statusCode: StatusCodes.Status200OK,
                code: ResponseCodeConstants.SUCCESS,
                data: "Friend added successfully!"));
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteFriend(string id)
        {
            await _friendService.DeleteFriend(id);
            return Ok(new BaseResponseModel<string>(
                statusCode: StatusCodes.Status200OK,
                code: ResponseCodeConstants.SUCCESS,
                data: "Unfriend successfully!"));
        }
    }
}
