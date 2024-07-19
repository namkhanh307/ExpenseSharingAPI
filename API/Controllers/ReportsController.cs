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
    public class ReportsController : ControllerBase
    {
        private readonly IReportService _reportService;
        public ReportsController(IReportService reportService)
        {
            _reportService = reportService;
        }
        [HttpGet]
        public IActionResult GetReports(string? groupId)
        {
            var result = _reportService.GetReports(groupId);
            return Ok(new BaseResponseModel<List<GetReportModel>>(
                statusCode: StatusCodes.Status200OK,
                code: ResponseCodeConstants.SUCCESS,
                data: result));
        }
        [HttpPost]
        public IActionResult PostReport(PostReportModel model)
        {
            _reportService.PostReport(model);
            return Ok(new BaseResponseModel<string>(
                statusCode: StatusCodes.Status200OK,
                code: ResponseCodeConstants.SUCCESS,
                data: "Them bao cong thanh cong"));
        }
        [HttpPut]
        public IActionResult PutReport(string id, PutReportModel model)
        {
            _reportService.PutReport(id, model);
            return Ok(new BaseResponseModel<string>(
                statusCode: StatusCodes.Status200OK,
                code: ResponseCodeConstants.SUCCESS,
                data: "Chinh sua bao cao thanh cong"));
        }
        [HttpDelete]
        public IActionResult DeleteReport(string id)
        {
            _reportService.DeleteReport(id);
            return Ok(new BaseResponseModel<string>(
                statusCode: StatusCodes.Status200OK,
                code: ResponseCodeConstants.SUCCESS,
                data: "Xoa bao cao thanh cong"));
        }
    }
}
