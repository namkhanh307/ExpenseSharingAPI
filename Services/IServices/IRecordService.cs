using Repositories.Entities;
using Repositories.ResponseModel.RecordModel;

namespace Services.IServices
{
    public interface IRecordService
    {
        void DeleteRecord(string id);
        List<GetRecordModel> GetRecord();
        void PostRecord(PostRecordModel model);
        void PutRecord(string id, PutRecordModel model);
    }
}
