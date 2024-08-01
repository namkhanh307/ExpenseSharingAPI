using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.IServices;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExcelsController : ControllerBase
    {
        public readonly IExcelService _excelService;
        public ExcelsController(IExcelService excelService)
        {
            _excelService = excelService;
        }
        [HttpGet]
        public async Task<IActionResult> LongTermExcel(string reportId)
        {
            var fileContent = await _excelService.LongTermExcel(reportId);
            var fileName = "LongTerm.xlsx";
            var contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

            var stream = new MemoryStream(fileContent);
            stream.Position = 0;

            return File(stream, contentType, fileName);
        }

    }
}
