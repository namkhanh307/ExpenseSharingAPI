using Core.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Repositories.ResponseModel.RecordModel;
using Services.IServices;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecordsController : ControllerBase
    {
        private readonly IRecordService _recordService;
        private readonly IWebHostEnvironment _env;
        public RecordsController(IRecordService recordService, IWebHostEnvironment env)
        {
            _recordService = recordService;
            _env = env;
        }
        [HttpGet("GetRecords")]
        public async Task<IActionResult> GetRecords(string? reportId)
        {
            var result = await _recordService.GetRecord(null, reportId);
            return Ok(new BaseResponseModel<List<GetRecordModel>>(
               statusCode: StatusCodes.Status200OK,
               code: ResponseCodeConstants.SUCCESS,
               data: result));
        }
        [HttpGet("GetRecordDetails")]
        public async Task<IActionResult> GetRecordDetails(string recordId)
        {
            var result = await _recordService.GetRecord(recordId, null);
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
               data: "Tạo mới bản ghi thành công"));
        }

        [HttpPut]
        public async Task<IActionResult> PutRecord(string id, PutRecordModel model)
        {
            await _recordService.PutRecord(id, model);
            return Ok(new BaseResponseModel<string>(
               statusCode: StatusCodes.Status200OK,
               code: ResponseCodeConstants.SUCCESS,
               data: "Chỉnh sửa bản ghi thành công"));
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteRecord(string id)
        {
            await _recordService.DeleteRecord(id);
            return Ok(new BaseResponseModel<string>(
               statusCode: StatusCodes.Status200OK,
               code: ResponseCodeConstants.SUCCESS,
               data: "Xóa bản ghi thành công"));
        }
    }
}
