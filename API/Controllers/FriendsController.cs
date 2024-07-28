﻿using Core.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repositories.ResponseModel.ExpenseModel;
using Repositories.ResponseModel.FriendModel;
using Services.IServices;
using System.Reflection.Metadata.Ecma335;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FriendsController : ControllerBase
    {
        private readonly IFriendService _friendService;
        public FriendsController(IFriendService friendService)
        {
            _friendService = friendService;
        }
        [HttpGet] 
        public IActionResult GetFriends()
        {
            var result = _friendService.GetFriends();
            return Ok(new BaseResponseModel<List<GetFriendModel>>(
                statusCode: StatusCodes.Status200OK,
                code: ResponseCodeConstants.SUCCESS,
                data: result));
        }
        [HttpPost]
        public IActionResult PostFriend(PostFriendModel model)
        {
            _friendService.PostFriend(model);
            return Ok(new BaseResponseModel<string>(
                statusCode: StatusCodes.Status200OK,
                code: ResponseCodeConstants.SUCCESS,
                data: "Add friend successfully!"));
        }
        [HttpDelete]
        public IActionResult DeleteFriend(string id)
        {
            _friendService.DeleteFriend(id);
            return Ok(new BaseResponseModel<string>(
                statusCode: StatusCodes.Status200OK,
                code: ResponseCodeConstants.SUCCESS,
                data: "Unfriend successfully!"));
        }
    }
}
