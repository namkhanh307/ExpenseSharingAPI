using Repositories.Entities;
using Repositories.ResponseModel.ReportModel;

namespace Services.IServices
{
    public interface IReportService
    {
        Task DeleteReport(string id);
        Task<List<GetReportModel>> GetReports(string? groupId);
        Task PostReport(PostReportModel model);
        Task PutReport(string id, PutReportModel model);
    }
}
