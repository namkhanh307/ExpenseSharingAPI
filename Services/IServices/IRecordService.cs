using Microsoft.AspNetCore.Http;
using Repositories.Entities;
using Repositories.ResponseModel.RecordModel;

namespace Services.IServices
{
    public interface IRecordService
    {
        void DeleteRecord(string id);
        List<GetRecordModel> GetRecord(string? recordId, string? reportId);
        Task PostRecord(PostRecordModel model);
        Task PutRecord(string id, PutRecordModel model);
    }
}
