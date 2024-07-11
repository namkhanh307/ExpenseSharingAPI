using Microsoft.AspNetCore.Mvc;
using Repositories.Entities;
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
        [HttpGet]
        public List<GetRecordModel> GetRecords()
        {
            return _recordService.GetRecord();
        }
        [HttpPost]
        public void PostRecord(PostRecordModel model)
        {
            _recordService.PostRecord(model);
        }
        [HttpPut]
        public void PutRecord(string id, PutRecordModel model)
        {
            _recordService.PutRecord(id, model);
        }
        [HttpDelete]
        public void DeleteRecord(string id)
        {
            _recordService.DeleteRecord(id);
        }
    }
}
