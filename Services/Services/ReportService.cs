using AutoMapper;
using Core.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Repositories.Entities;
using Repositories.IRepositories;
using Repositories.ResponseModel.ExpenseModel;
using Repositories.ResponseModel.ReportModel;
using Services.IServices;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Services
{
    public class ReportService : IReportService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        private string currentUserId => Authentication.GetUserIdFromHttpContextAccessor(_httpContextAccessor);

        public ReportService(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<List<GetReportModel>> GetReports(string? groupId)
        {
            List<Report> reports = await _unitOfWork.GetRepository<Report>()
                .Entities
                .Where(g => !g.DeletedTime.HasValue)
                .ToListAsync();

            if (!string.IsNullOrWhiteSpace(groupId))
            {
                reports = reports.Where(r => r.GroupId == groupId).ToList();
            }

            List<GetReportModel> result = new List<GetReportModel>();
            foreach (var report in reports)
            {
                var group = await _unitOfWork.GetRepository<Group>().GetByIdAsync(report.GroupId);
                if (group == null)
                {
                    throw new ErrorException(StatusCodes.Status404NotFound, ErrorCode.NotFound, "Nhóm không tìm thấy.");
                }

                GetReportModel response = new GetReportModel
                {
                    Id = report.Id,
                    GroupId = report.GroupId,
                    Name = report.Name,
                    GroupName = group.Name,
                };
                result.Add(response);
            }
            return result;
        }

        public async Task PostReport(PostReportModel model)
        {
            var report = _mapper.Map<Report>(model);
            report.CreatedTime = DateTime.Now;
            report.CreatedBy = currentUserId;

            await _unitOfWork.GetRepository<Report>().InsertAsync(report);
            await _unitOfWork.SaveAsync();
        }

        public async Task PutReport(string id, PutReportModel model)
        {
            Report? existedReport = await _unitOfWork.GetRepository<Report>().GetByIdAsync(id);

            if (existedReport == null)
            {
                throw new ErrorException(StatusCodes.Status404NotFound, ErrorCode.NotFound, "Báo cáo không tìm thấy.");
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
                throw new ErrorException(StatusCodes.Status404NotFound, ErrorCode.NotFound, "Báo cáo không tìm thấy.");
            }

            existedReport.DeletedTime = DateTime.Now;
            existedReport.DeletedBy = currentUserId;

            await _unitOfWork.GetRepository<Report>().UpdateAsync(existedReport);
            await _unitOfWork.SaveAsync();
        }
    }
}
