using Core.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Repositories.Entities;
using Repositories.ResponseModel.GroupModel;
using Repositories.ResponseModel.ReportModel;
using Services.IServices;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController(IReportService reportService) : ControllerBase
    {
        private readonly IReportService _reportService = reportService;

        [HttpGet]
        public async Task<IActionResult> GetReports(string? groupId)
        {
            var result = await _reportService.GetReports(groupId);
            return Ok(new BaseResponseModel<List<GetReportModel>>(
                statusCode: StatusCodes.Status200OK,
                code: ResponseCodeConstants.SUCCESS,
                data: result));
        }
        [HttpPost]
        public async Task<IActionResult> PostReport(PostReportModel model)
        {
            await _reportService.PostReport(model);
            return Ok(new BaseResponseModel<string>(
                statusCode: StatusCodes.Status200OK,
                code: ResponseCodeConstants.SUCCESS,
                data: "Report added SUCCESSFULLY!"));
        }
        [HttpPut]
        public async Task<IActionResult> PutReport(string id, PutReportModel model)
        {
            await _reportService.PutReport(id, model);
            return Ok(new BaseResponseModel<string>(
                statusCode: StatusCodes.Status200OK,
                code: ResponseCodeConstants.SUCCESS,
                data: "Report modified SUCCESSFULLY!"));
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteReport(string id)
        {
            await _reportService.DeleteReport(id);
            return Ok(new BaseResponseModel<string>(
                statusCode: StatusCodes.Status200OK,
                code: ResponseCodeConstants.SUCCESS,
                data: "Report deleted SUCCESSFULLY!"));
        }
    }
}
