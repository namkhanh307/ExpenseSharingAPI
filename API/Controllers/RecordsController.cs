using Core.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repositories.Entities;
using Repositories.ResponseModel.GroupModel;
using Repositories.ResponseModel.RecordModel;
using Services.IServices;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecordsController(IRecordService recordService) : ControllerBase
    {
        private readonly IRecordService _recordService = recordService;

        [HttpGet("GetRecords")]
        public async Task<IActionResult> GetRecords(string? reportId)
        {
            List<GetRecordModel> result = await _recordService.GetRecord(null, reportId);
            return Ok(new BaseResponseModel<List<GetRecordModel>>(
               statusCode: StatusCodes.Status200OK,
               code: ResponseCodeConstants.SUCCESS,
               data: result));
        }
        [HttpGet("GetRecordDetails")]
        public async Task<IActionResult> GetRecordDetails(string recordId)
        {
            List<GetRecordModel> result = await _recordService.GetRecord(recordId, null);
            return Ok(new BaseResponseModel<List<GetRecordModel>>(
               statusCode: StatusCodes.Status200OK,
               code: ResponseCodeConstants.SUCCESS,
               data: result));
        }
        [HttpPost]
        public async Task<IActionResult> PostRecord(PostRecordModel model)
        {
            await _recordService.PostRecord(model);
            return Ok(new BaseResponseModel<string>(
               statusCode: StatusCodes.Status200OK,
               code: ResponseCodeConstants.SUCCESS,
               data: "Record added SUCCESSFULLY!"));
        }

        [HttpPut]
        public async Task<IActionResult> PutRecord(string id, PutRecordModel model)
        {
            await _recordService.PutRecord(id, model);
            return Ok(new BaseResponseModel<string>(
               statusCode: StatusCodes.Status200OK,
               code: ResponseCodeConstants.SUCCESS,
               data: "Record modified SUCCESSFULLY!"));
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteRecord(string id)
        {
            await _recordService.DeleteRecord(id);
            return Ok(new BaseResponseModel<string>(
               statusCode: StatusCodes.Status200OK,
               code: ResponseCodeConstants.SUCCESS,
               data: "Record deleted SUCCESSFULLY!"));
        }
    }
}
