using AutoMapper;
using Repositories.Entities;
using Repositories.IRepositories;
using Repositories.ResponseModel.ReportModel;
using Services.IServices;

namespace Services.Services
{
    public class ReportService : IReportService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ReportService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public void DeleteReport(string id)
        {
            throw new NotImplementedException();
        }

        public List<Report> GetReports()
        {
            throw new NotImplementedException();
        }

        public void PostReport(PostReportModel model)
        {
            throw new NotImplementedException();
        }

        public void PutReport(string id, PutReportModel model)
        {
            throw new NotImplementedException();
        }
    }
}
