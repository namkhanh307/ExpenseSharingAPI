using Core.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Repositories.ResponseModel.FriendRequestModel;
using Services.IServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FriendRequestsController : ControllerBase
    {
        private readonly IFriendRequestService _friendRequestService;
        private readonly ILogger<FriendRequestsController> _logger;

        public FriendRequestsController(IFriendRequestService friendRequestService, ILogger<FriendRequestsController> logger)
        {
            _friendRequestService = friendRequestService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetFriendRequest()
        {
            var result = await _friendRequestService.GetFriendRequest();
            return Ok(new BaseResponseModel<List<GetFriendRequestModel>>(
                statusCode: StatusCodes.Status200OK,
                code: ResponseCodeConstants.SUCCESS,
                data: result));
        }

        [HttpPost]
        public IActionResult PostFriendRequest(string receiveId)
        {
            _friendRequestService.PostFriendRequest(receiveId);
            return Ok(new BaseResponseModel<string>(
                statusCode: StatusCodes.Status200OK,
                code: ResponseCodeConstants.SUCCESS,
                data: "Friend request added SUCCESSFULLY!"));
        }

        [HttpPut]
        public IActionResult PutFriendRequest(string id)
        {
            _friendRequestService.AcceptFriendRequest(id);
            return Ok(new BaseResponseModel<string>(
                statusCode: StatusCodes.Status200OK,
                code: ResponseCodeConstants.SUCCESS,
                data: "Friend request accepted SUCCESSFULLY!"));
        }

        [HttpDelete]
        public IActionResult DeleteFriendRequest(string id)
        {
            _friendRequestService.RejectFriendRequest(id);
            return Ok(new BaseResponseModel<string>(
                statusCode: StatusCodes.Status200OK,
                code: ResponseCodeConstants.SUCCESS,
                data: "Friend request deleted SUCCESSFULLY!"));
        }
    }
}
