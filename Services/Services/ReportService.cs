﻿using AutoMapper;
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

        public List<Report> GetReports()
        {
            return _unitOfWork.GetRepository<Report>().Entities.Where(g => !g.DeletedTime.HasValue).ToList();
        }

        public void PostReport(PostReportModel model)
        {
            var report = _mapper.Map<Report>(model);
            report.CreatedTime = DateTime.Now;
            _unitOfWork.GetRepository<Report>().Insert(report);
            _unitOfWork.Save();
        }

        public void PutReport(string id, PutReportModel model)
        {
            var existedReport = _unitOfWork.GetRepository<Report>().GetById(id);
            if (existedReport == null)
            {
                throw new Exception($"Group with ID {id} doesn't exist!");
            }
            _mapper.Map(model, existedReport);
            existedReport.LastUpdatedTime = DateTime.Now;
            _unitOfWork.GetRepository<Report>().Update(existedReport);
            _unitOfWork.Save();
        }

        public void DeleteReport(string id)
        {
            var existedReport = _unitOfWork.GetRepository<Report>().GetById(id);
            if (existedReport == null)
            {
                throw new Exception($"Group with ID {id} doesn't exist!");
            }
            existedReport.DeletedTime = DateTime.Now;
            _unitOfWork.GetRepository<Report>().Update(existedReport);
            _unitOfWork.Save();
        }
    }
}
