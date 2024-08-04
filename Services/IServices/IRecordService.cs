using Repositories.Entities;
using Repositories.ResponseModel.RecordModel;

namespace Services.IServices
{
    public interface IRecordService
    {
        Task<List<GetRecordModel>> GetRecord(string? recordId, string? reportId);
        Task PostRecord(PostRecordModel model);
        Task PutRecord(PutRecordModel model);
        Task DeleteRecord(string recordId);
    }
}
