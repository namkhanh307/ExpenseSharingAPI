using Repositories.Entities;
using Repositories.ResponseModel.ReportModel;

namespace Services.IServices
{
    public interface IReportService
    {
        void DeleteReport(string id);
        List<GetReportModel> GetReports();
        void PostReport(PostReportModel model);
        void PutReport(string id, PutReportModel model);
    }
}
