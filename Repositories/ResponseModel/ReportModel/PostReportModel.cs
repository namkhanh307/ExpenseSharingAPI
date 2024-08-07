
namespace Repositories.ResponseModel.ReportModel
{
    public class PostReportModel
    {
        public string? Name { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public string GroupId { get; set; }
    }
}
