using AutoMapper;
using Core.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Repositories.Entities;
using Repositories.IRepositories;
using Repositories.ResponseModel.ExpenseModel;
using Repositories.ResponseModel.ReportModel;
using Services.IServices;

namespace Services.Services
{
    public class ReportService(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor) : IReportService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
        private string currentUserId => Authentication.GetUserIdFromHttpContextAccessor(_httpContextAccessor);

        public async Task<List<GetReportModel>> GetReports(string? groupId)
        {
            List<Report> reports = await _unitOfWork.GetRepository<Report>().Entities.Where(g => !g.DeletedTime.HasValue).ToListAsync();
            if(!string.IsNullOrWhiteSpace(groupId))
            {
                reports = reports.Where(r => r.GroupId == groupId).ToList();
            }
            List<GetReportModel> result = [];
            foreach (var report in reports)
            {
                GetReportModel response = new()
                {
                    Id = report.Id,
                    GroupId = report.GroupId,
                    Name = report.Name,
                    GroupName = _unitOfWork.GetRepository<Group>().GetById(report.GroupId).Name,
                };
                result.Add(response);
            }
            return result;
        }

        public async Task PostReport(PostReportModel model)
        {
            var report = _mapper.Map<Report>(model);
            report.CreatedTime = DateTime.Now;
            await _unitOfWork.GetRepository<Report>().InsertAsync(report);
            await _unitOfWork.SaveAsync();
        }

        public async Task PutReport(string id, PutReportModel model)
        {
            Report? existedReport = await _unitOfWork.GetRepository<Report>().GetByIdAsync(id);
            if (existedReport == null)
            {
                throw new Exception($"Report with ID {id} doesn't exist!");
            }
            _mapper.Map(model, existedReport);
            existedReport.LastUpdatedTime = DateTime.Now;
            existedReport.LastUpdatedBy = currentUserId;
            await _unitOfWork.GetRepository<Report>().UpdateAsync(existedReport);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteReport(string id)
        {
            Report? existedReport = await _unitOfWork.GetRepository<Report>().GetByIdAsync(id);
            if (existedReport == null)
            {
                throw new Exception($"Report with ID {id} doesn't exist!");
            }
            existedReport.DeletedTime = DateTime.Now;
            existedReport.DeletedBy = currentUserId;
            await _unitOfWork.GetRepository<Report>().UpdateAsync(existedReport);
            await _unitOfWork.SaveAsync();
        }
    }
}
