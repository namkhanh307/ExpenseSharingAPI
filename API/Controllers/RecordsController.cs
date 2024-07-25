using Core.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Repositories.Entities;
using Repositories.ResponseModel.GroupModel;
using Repositories.ResponseModel.RecordModel;
using Services.IServices;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecordsController : ControllerBase
    {
        private readonly IRecordService _recordService;
        public RecordsController(IRecordService recordService)
        {
            _recordService = recordService;
        }

        [HttpGet("GetRecords")]
        public IActionResult GetRecords(string? reportId)
        {
            var result = _recordService.GetRecord(null, reportId);
            return Ok(new BaseResponseModel<List<GetRecordModel>>(
               statusCode: StatusCodes.Status200OK,
               code: ResponseCodeConstants.SUCCESS,
               data: result));
        }

        [HttpGet("GetRecordDetails")]
        public IActionResult GetRecordDetails(string recordId)
        {
            var result = _recordService.GetRecord(recordId, null);
            return Ok(new BaseResponseModel<List<GetRecordModel>>(
               statusCode: StatusCodes.Status200OK,
               code: ResponseCodeConstants.SUCCESS,
               data: result));
        }

        [HttpPost]
        public IActionResult PostRecord(PostRecordModel model)
        {
            _recordService.PostRecord(model);
            return Ok(new BaseResponseModel<string>(
               statusCode: StatusCodes.Status200OK,
               code: ResponseCodeConstants.SUCCESS,
               data: "Tao moi ban ghi thanh cong"));
        }
        [HttpPut]
        public IActionResult PutRecord(string id, PutRecordModel model)
        {
            _recordService.PutRecord(id, model);
            return Ok(new BaseResponseModel<string>(
               statusCode: StatusCodes.Status200OK,
               code: ResponseCodeConstants.SUCCESS,
               data: "Chinh sua ban ghi thanh cong"));
        }
        [HttpDelete]
        public IActionResult DeleteRecord(string id)
        {
            _recordService.DeleteRecord(id);
            return Ok(new BaseResponseModel<string>(
               statusCode: StatusCodes.Status200OK,
               code: ResponseCodeConstants.SUCCESS,
               data: "Xoa ban ghi thanh cong"));
        }
    }
}
