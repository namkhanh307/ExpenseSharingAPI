using Microsoft.AspNetCore.Mvc;
using Repositories.Entities;
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
        public List<Report> GetReports()
        {
            return _reportService.GetReports();
        }
        [HttpPost]
        public void PostReport(PostReportModel model)
        {
            _reportService.PostReport(model);
        }
        [HttpPut]
        public void PutReport(string id, PutReportModel model)
        {
            _reportService.PutReport(id, model);
        }
        [HttpDelete]
        public void DeleteReport(string id)
        {
            _reportService.DeleteReport(id);
        }
    }
}
