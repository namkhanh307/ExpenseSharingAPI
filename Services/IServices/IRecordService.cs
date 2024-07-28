using Repositories.Entities;
using Repositories.ResponseModel.RecordModel;

namespace Services.IServices
{
    public interface IRecordService
    {
        List<GetRecordModel> GetRecord(string? recordId, string? reportId);
        Task PostRecord(PostRecordModel model);
        Task PutRecord(string id, PutRecordModel model);
        Task DeleteRecord(string id);
    }
}
